import { useEffect, useState } from 'react';
import { ApiError, api, errorText } from '../api.js';
import { Failure, Field, Loading, useConfirm, useToast } from '../components/Ui.jsx';
import { localNow, toIso } from '../format.js';
import { TransactionRows } from './Dashboard.jsx';

export function Transactions() {
  const toast = useToast();
  const ask = useConfirm();
  const [page, setPage] = useState(1);
  const [type, setType] = useState('');
  const [data, setData] = useState(null);
  const [error, setError] = useState('');
  const [errors, setErrors] = useState(null);
  const [entryType, setEntryType] = useState('Expense');

  const load = () => {
    setError('');
    Promise.all([
      api('/api/v1/accounts'),
      api('/api/v1/categories'),
      api('/api/v1/transactions', { query: { page, pageSize: 20, type } }),
    ]).then(([accounts, categories, pageData]) => {
      setData({ accounts, categories, pageData });
    }).catch((reason) => setError(errorText(reason)));
  };

  useEffect(load, [page, type]);

  if (error) return <Failure message={error} />;
  if (!data) return <Loading />;

  const names = Object.fromEntries(data.categories.map((category) => [category.id, category.name]));
  const pages = Math.max(1, Math.ceil(data.pageData.totalCount / data.pageData.pageSize));
  const categories = data.categories.filter((category) => category.type === entryType);

  const remove = async (id) => {
    const accepted = await ask('Excluir esta transação? O saldo da conta será ajustado.');
    if (!accepted) return;
    await api(`/api/v1/transactions/${id}`, { method: 'DELETE' });
    toast('Transação excluída.');
    load();
  };

  const submit = async (event) => {
    event.preventDefault();
    const values = Object.fromEntries(new FormData(event.currentTarget));
    setErrors(null);
    try {
      await api('/api/v1/transactions', {
        method: 'POST',
        body: { ...values, amount: Number(values.amount), transactionDate: toIso(values.transactionDate) },
      });
      event.currentTarget.reset();
      setEntryType('Expense');
      toast('Transação registrada.');
      setPage(1);
      load();
    } catch (reason) {
      if (reason instanceof ApiError) setErrors(reason.body?.errors || null);
      toast(errorText(reason), 'error');
    }
  };

  return (
    <div className="layout">
      <section>
        <div className="section-head">
          <h2>Transações</h2>
          <select className="auto" aria-label="Filtrar por tipo" value={type} onChange={(event) => { setPage(1); setType(event.target.value); }}>
            <option value="">Todas</option>
            <option value="Expense">Despesas</option>
            <option value="Income">Receitas</option>
          </select>
        </div>
        <TransactionRows items={data.pageData.items} names={names} onDelete={remove} />
        <div className="pager">
          <button type="button" className="quiet" disabled={data.pageData.page <= 1} onClick={() => setPage((current) => current - 1)}>Anterior</button>
          <span className="muted">Página {data.pageData.page} de {pages}</span>
          <button type="button" className="quiet" disabled={data.pageData.page >= pages} onClick={() => setPage((current) => current + 1)}>Próxima</button>
        </div>
      </section>
      <form className="panel" onSubmit={submit}>
        <h2>Nova transação</h2>
        <div className="seg">
          <label><input type="radio" name="type" value="Expense" checked={entryType === 'Expense'} onChange={() => setEntryType('Expense')} /> Despesa</label>
          <label><input type="radio" name="type" value="Income" checked={entryType === 'Income'} onChange={() => setEntryType('Income')} /> Receita</label>
        </div>
        <Field label="Conta" name="accountId" errors={errors}>
          <select name="accountId" required defaultValue="">
            {data.accounts.length ? data.accounts.map((account) => <option key={account.id} value={account.id}>{account.name}</option>) : <option value="">Crie uma conta primeiro</option>}
          </select>
        </Field>
        <Field label="Categoria" name="categoryId" errors={errors}>
          <select name="categoryId" key={entryType} required defaultValue={categories[0]?.id || ''}>
            {categories.length ? categories.map((category) => <option key={category.id} value={category.id}>{category.name}</option>) : <option value="">Crie uma categoria primeiro</option>}
          </select>
        </Field>
        <Field label="Valor" name="amount" errors={errors}>
          <input name="amount" type="number" step="0.01" min="0.01" required />
        </Field>
        <Field label="Descrição" name="description" errors={errors}>
          <input name="description" maxLength={200} required />
        </Field>
        <Field label="Data, se não for agora" name="transactionDate" errors={errors}>
          <input name="transactionDate" type="datetime-local" max={localNow()} />
        </Field>
        <button className="primary" type="submit">Registrar transação</button>
      </form>
    </div>
  );
}
