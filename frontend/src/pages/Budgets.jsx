import { useEffect, useState } from 'react';
import { ApiError, api, errorText } from '../api.js';
import { Failure, Field, Loading, Meter, useConfirm, useToast } from '../components/Ui.jsx';
import { money, periodLabels } from '../format.js';

export function Budgets() {
  const toast = useToast();
  const ask = useConfirm();
  const [data, setData] = useState(null);
  const [error, setError] = useState('');
  const [errors, setErrors] = useState(null);

  const load = () => {
    Promise.all([
      api('/api/v1/budgets'),
      api('/api/v1/categories', { query: { type: 'Expense' } }),
    ]).then(async ([budgets, categories]) => {
      const statuses = await Promise.all(budgets.map((budget) => api(`/api/v1/budgets/${budget.id}/status`)));
      setData({ budgets, categories, statuses });
    }).catch((reason) => setError(errorText(reason)));
  };

  useEffect(() => { load(); }, []);

  if (error) return <Failure message={error} />;
  if (!data) return <Loading />;

  const names = Object.fromEntries(data.categories.map((category) => [category.id, category.name]));

  const remove = async (id) => {
    const accepted = await ask('Excluir este orçamento?');
    if (!accepted) return;
    await api(`/api/v1/budgets/${id}`, { method: 'DELETE' });
    toast('Orçamento excluído.');
    load();
  };

  const submit = async (event) => {
    event.preventDefault();
    const values = Object.fromEntries(new FormData(event.currentTarget));
    setErrors(null);
    try {
      await api('/api/v1/budgets', {
        method: 'POST',
        body: {
          ...values,
          limitAmount: Number(values.limitAmount),
          alertThreshold: Number(values.alertThreshold),
          notes: values.notes || null,
        },
      });
      event.currentTarget.reset();
      toast('Orçamento criado.');
      load();
    } catch (reason) {
      if (reason instanceof ApiError) setErrors(reason.body?.errors || null);
      toast(errorText(reason), 'error');
    }
  };

  return (
    <div className="layout">
      <section>
        <div className="section-head"><h2>Orçamentos</h2></div>
        {data.budgets.length === 0 && <p className="empty">Nenhum orçamento ainda. Defina um limite por categoria.</p>}
        {data.budgets.map((budget, index) => {
          const status = data.statuses[index];
          const tone = status.isExceeded ? 'over' : status.isOverThreshold ? 'warn' : '';
          return (
            <article className="account" key={budget.id}>
              <div className="between">
                <strong>{names[budget.categoryId] || 'Categoria'}</strong>
                <span className="muted">{periodLabels[budget.period]}</span>
              </div>
              <Meter value={status.percentUsed} tone={tone} />
              <p>{money(status.spent)} de {money(status.limitAmount)} <span className="muted">{status.percentUsed}%</span></p>
              <div className="between">
                <span className="muted">{status.isExceeded ? `Excedido em ${money(-status.remaining)}` : `Restam ${money(status.remaining)}`}</span>
                <button type="button" className="ghost" onClick={() => remove(budget.id)}>Excluir</button>
              </div>
            </article>
          );
        })}
      </section>
      <form className="panel" onSubmit={submit}>
        <h2>Novo orçamento</h2>
        <Field label="Categoria" name="categoryId" errors={errors}>
          <select name="categoryId" required defaultValue="">
            {data.categories.length
              ? data.categories.map((category) => <option key={category.id} value={category.id}>{category.name}</option>)
              : <option value="">Crie uma categoria de despesa primeiro</option>}
          </select>
        </Field>
        <Field label="Limite" name="limitAmount" errors={errors}>
          <input name="limitAmount" type="number" step="0.01" min="0.01" required />
        </Field>
        <Field label="Alertar em (%)" name="alertThreshold" errors={errors}>
          <input name="alertThreshold" type="number" min="1" max="100" defaultValue="80" required />
        </Field>
        <Field label="Período" name="period" errors={errors}>
          <select name="period" defaultValue="Monthly">
            {Object.entries(periodLabels).map(([value, label]) => <option key={value} value={value}>{label}</option>)}
          </select>
        </Field>
        <Field label="Observações" name="notes" errors={errors}>
          <input name="notes" maxLength={500} />
        </Field>
        <button className="primary" type="submit">Criar orçamento</button>
      </form>
    </div>
  );
}
