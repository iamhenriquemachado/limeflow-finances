import { useEffect, useState } from 'react';
import { api, errorText } from '../api.js';
import { Failure, Loading } from '../components/Ui.jsx';
import { formatDate, money, months, startOfMonth } from '../format.js';

export function Dashboard() {
  const [data, setData] = useState(null);
  const [error, setError] = useState('');

  useEffect(() => {
    const year = new Date().getFullYear();
    Promise.all([
      api('/api/v1/reports/summary', { query: { from: startOfMonth(), to: new Date().toISOString() } }),
      api('/api/v1/reports/monthly', { query: { year } }),
      api('/api/v1/categories'),
      api('/api/v1/transactions', { query: { pageSize: 5 } }),
    ]).then(([summary, monthly, categories, recent]) => {
      setData({ summary, monthly, categories, recent, year });
    }).catch((reason) => setError(errorText(reason)));
  }, []);

  if (error) return <Failure message={error} />;
  if (!data) return <Loading />;

  const names = Object.fromEntries(data.categories.map((category) => [category.id, category.name]));
  const max = Math.max(1, ...data.monthly.flatMap((month) => [month.income, month.expense]));
  const monthName = new Date().toLocaleDateString('pt-BR', { month: 'long', year: 'numeric' });

  return (
    <>
      <p className="when">{monthName}</p>
      <p className="balance">{money(data.summary.net)}</p>
      <p className="caption">Resultado do mês, receitas menos despesas.</p>
      <div className="split">
        <div><span>Receitas</span><strong className="income">{money(data.summary.totalIncome)}</strong></div>
        <div><span>Despesas</span><strong className="expense">{money(data.summary.totalExpense)}</strong></div>
        <div><span>Saldo das contas</span><strong>{money(data.summary.totalBalance)}</strong></div>
      </div>

      <section className="year">
        <h2>{data.year}</h2>
        <Stems monthly={data.monthly} max={max} />
      </section>

      <div className="section-head"><h2>Últimas movimentações</h2></div>
      <TransactionRows items={data.recent.items} names={names} />
    </>
  );
}

function Stems({ monthly, max }) {
  const [grown, setGrown] = useState(false);
  useEffect(() => {
    const frame = requestAnimationFrame(() => setGrown(true));
    return () => cancelAnimationFrame(frame);
  }, [monthly]);

  return (
    <div className="stems">
      {monthly.map((month) => (
        <div className="stem" key={month.month} title={`${months[month.month - 1]}: receitas ${money(month.income)}, despesas ${money(month.expense)}`}>
          <span className="pair">
            <i className="inc" style={{ height: grown ? `${(month.income / max) * 120}px` : '2px' }} />
            <i className="exp" style={{ height: grown ? `${(month.expense / max) * 120}px` : '2px' }} />
          </span>
          <span className="axis">{months[month.month - 1]}</span>
        </div>
      ))}
    </div>
  );
}

export function TransactionRows({ items, names, onDelete }) {
  if (!items.length) return <p className="empty">Nenhuma transação ainda. Registre a primeira.</p>;
  return (
    <ul className="rows">
      {items.map((transaction) => (
        <li className="row" key={transaction.id}>
          <div className="who">
            <strong>{transaction.description}</strong>
            <span className="muted">{names[transaction.categoryId] || 'Sem categoria'} {formatDate(transaction.transactionDate)}</span>
          </div>
          <div className="end">
            <span className={`amount ${transaction.type === 'Income' ? 'income' : 'expense'}`}>
              {transaction.type === 'Income' ? '+' : '−'} {money(transaction.amount)}
            </span>
            {onDelete && <button type="button" className="ghost" onClick={() => onDelete(transaction.id)}>Excluir</button>}
          </div>
        </li>
      ))}
    </ul>
  );
}
