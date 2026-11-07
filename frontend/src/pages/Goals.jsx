import { useEffect, useState } from 'react';
import { ApiError, api, errorText } from '../api.js';
import { Failure, Field, Loading, Meter, useConfirm, useToast } from '../components/Ui.jsx';
import { formatDate, icons, money, toIso, tomorrow } from '../format.js';

export function Goals() {
  const toast = useToast();
  const ask = useConfirm();
  const [data, setData] = useState(null);
  const [error, setError] = useState('');
  const [errors, setErrors] = useState(null);

  const load = () => {
    Promise.all([api('/api/v1/goals'), api('/api/v1/accounts')])
      .then(([goals, accounts]) => setData({ goals, accounts }))
      .catch((reason) => setError(errorText(reason)));
  };

  useEffect(() => { load(); }, []);

  if (error) return <Failure message={error} />;
  if (!data) return <Loading />;

  const remove = async (id) => {
    const accepted = await ask('Excluir esta meta? O valor guardado será perdido.');
    if (!accepted) return;
    await api(`/api/v1/goals/${id}`, { method: 'DELETE' });
    toast('Meta excluída.');
    load();
  };

  const create = async (event) => {
    event.preventDefault();
    const values = Object.fromEntries(new FormData(event.currentTarget));
    setErrors(null);
    try {
      await api('/api/v1/goals', {
        method: 'POST',
        body: {
          ...values,
          target: Number(values.target),
          monthlyContribution: Number(values.monthlyContribution),
          targetDate: toIso(`${values.targetDate}T00:00`),
        },
      });
      event.currentTarget.reset();
      toast('Meta criada.');
      load();
    } catch (reason) {
      if (reason instanceof ApiError) setErrors(reason.body?.errors || null);
      toast(errorText(reason), 'error');
    }
  };

  const contribute = async (event, id) => {
    event.preventDefault();
    const values = Object.fromEntries(new FormData(event.currentTarget));
    try {
      await api(`/api/v1/goals/${id}/contributions`, {
        method: 'POST',
        body: { amount: Number(values.amount), fromAccountId: values.fromAccountId || null },
      });
      event.currentTarget.reset();
      toast('Aporte registrado.');
      load();
    } catch (reason) {
      toast(errorText(reason), 'error');
    }
  };

  const withdraw = async (event, id) => {
    event.preventDefault();
    const values = Object.fromEntries(new FormData(event.currentTarget));
    try {
      await api(`/api/v1/goals/${id}/withdrawals`, { method: 'POST', body: { amount: Number(values.amount) } });
      event.currentTarget.reset();
      toast('Retirada registrada.');
      load();
    } catch (reason) {
      toast(errorText(reason), 'error');
    }
  };

  return (
    <div className="layout">
      <section>
        <div className="section-head"><h2>Metas</h2></div>
        {data.goals.length === 0 && <p className="empty">Nenhuma meta ainda. Crie uma para começar a guardar.</p>}
        {data.goals.map((goal) => (
          <article className="account stack" key={goal.id}>
            <div className="between">
              <strong>{goal.icon} {goal.name}</strong>
              {goal.isCompleted && <span className="tag">Concluída</span>}
            </div>
            <p className="muted">{goal.description}</p>
            <Meter value={goal.progressPercent} tone="goal" />
            <p>{money(goal.alreadySaved)} de {money(goal.target)} <span className="muted">{Math.floor(goal.progressPercent)}%</span></p>
            <p className="muted">Até {formatDate(goal.targetDate)}. Aporte mensal de {money(goal.monthlyContribution)}.</p>
            <form className="inline" onSubmit={(event) => contribute(event, goal.id)}>
              <input name="amount" type="number" step="0.01" min="0.01" placeholder="Valor" aria-label="Valor do aporte" required />
              <select name="fromAccountId" aria-label="Conta de origem" defaultValue="">
                <option value="">Não debitar de conta</option>
                {data.accounts.map((account) => <option key={account.id} value={account.id}>{account.name}</option>)}
              </select>
              <button className="quiet" type="submit">Aportar</button>
            </form>
            <form className="inline" onSubmit={(event) => withdraw(event, goal.id)}>
              <input name="amount" type="number" step="0.01" min="0.01" placeholder="Valor" aria-label="Valor da retirada" required />
              <button className="quiet" type="submit">Retirar</button>
            </form>
            <button type="button" className="ghost" onClick={() => remove(goal.id)}>Excluir meta</button>
          </article>
        ))}
      </section>
      <form className="panel" onSubmit={create}>
        <h2>Nova meta</h2>
        <Field label="Nome" name="name" errors={errors}>
          <input name="name" maxLength={100} required />
        </Field>
        <Field label="Descrição" name="description" errors={errors}>
          <input name="description" maxLength={500} required />
        </Field>
        <Field label="Valor alvo" name="target" errors={errors}>
          <input name="target" type="number" step="0.01" min="0.01" required />
        </Field>
        <Field label="Data limite" name="targetDate" errors={errors}>
          <input name="targetDate" type="date" min={tomorrow()} required />
        </Field>
        <Field label="Aporte mensal" name="monthlyContribution" errors={errors}>
          <input name="monthlyContribution" type="number" step="0.01" min="0" required />
        </Field>
        <Field label="Ícone" name="icon" errors={errors}>
          <select name="icon" defaultValue={icons[0]}>
            {icons.map((icon) => <option key={icon}>{icon}</option>)}
          </select>
        </Field>
        <button className="primary" type="submit">Criar meta</button>
      </form>
    </div>
  );
}
