import { useEffect, useState } from 'react';
import { ApiError, api, errorText } from '../api.js';
import { Failure, Field, Loading, useConfirm, useToast } from '../components/Ui.jsx';
import { typeLabels } from '../format.js';

export function Categories() {
  const toast = useToast();
  const ask = useConfirm();
  const [categories, setCategories] = useState(null);
  const [error, setError] = useState('');
  const [errors, setErrors] = useState(null);

  const load = () => api('/api/v1/categories').then(setCategories).catch((reason) => setError(errorText(reason)));
  useEffect(() => { load(); }, []);

  if (error) return <Failure message={error} />;
  if (!categories) return <Loading />;

  const remove = async (id) => {
    const accepted = await ask('Excluir esta categoria?');
    if (!accepted) return;
    try {
      await api(`/api/v1/categories/${id}`, { method: 'DELETE' });
      toast('Categoria excluída.');
      load();
    } catch (reason) {
      toast(errorText(reason), 'error');
    }
  };

  const submit = async (event) => {
    event.preventDefault();
    setErrors(null);
    try {
      await api('/api/v1/categories', { method: 'POST', body: Object.fromEntries(new FormData(event.currentTarget)) });
      event.currentTarget.reset();
      toast('Categoria criada.');
      load();
    } catch (reason) {
      if (reason instanceof ApiError) setErrors(reason.body?.errors || null);
      toast(errorText(reason), 'error');
    }
  };

  const group = (type) => {
    const items = categories.filter((category) => category.type === type);
    return (
      <section>
        <div className="section-head"><h2>{typeLabels[type]}</h2></div>
        {items.length === 0 && <p className="empty">Nenhuma ainda.</p>}
        <ul className="rows">
          {items.map((category) => (
            <li className="row" key={category.id}>
              <strong>{category.name}</strong>
              <button type="button" className="ghost" onClick={() => remove(category.id)}>Excluir</button>
            </li>
          ))}
        </ul>
      </section>
    );
  };

  return (
    <div className="layout">
      <div className="stack">
        {group('Income')}
        {group('Expense')}
      </div>
      <form className="panel" onSubmit={submit}>
        <h2>Nova categoria</h2>
        <Field label="Nome" name="name" errors={errors}>
          <input name="name" maxLength={50} required />
        </Field>
        <Field label="Tipo" name="type" errors={errors}>
          <select name="type" defaultValue="Expense">
            <option value="Expense">Despesa</option>
            <option value="Income">Receita</option>
          </select>
        </Field>
        <button className="primary" type="submit">Criar categoria</button>
      </form>
    </div>
  );
}
