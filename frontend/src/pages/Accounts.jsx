import { useEffect, useState } from 'react';
import { ApiError, api, errorText } from '../api.js';
import { Failure, Field, Loading, useConfirm, useToast } from '../components/Ui.jsx';
import { money } from '../format.js';

export function Accounts() {
  const toast = useToast();
  const ask = useConfirm();
  const [accounts, setAccounts] = useState(null);
  const [error, setError] = useState('');
  const [errors, setErrors] = useState(null);

  const load = () => api('/api/v1/accounts').then(setAccounts).catch((reason) => setError(errorText(reason)));
  useEffect(() => { load(); }, []);

  if (error) return <Failure message={error} />;
  if (!accounts) return <Loading />;

  const remove = async (id) => {
    const accepted = await ask('Excluir a conta? As transações dela também serão excluídas.');
    if (!accepted) return;
    await api(`/api/v1/accounts/${id}`, { method: 'DELETE' });
    toast('Conta excluída.');
    load();
  };

  const submit = async (event) => {
    event.preventDefault();
    setErrors(null);
    try {
      await api('/api/v1/accounts', { method: 'POST', body: Object.fromEntries(new FormData(event.currentTarget)) });
      event.currentTarget.reset();
      toast('Conta criada.');
      load();
    } catch (reason) {
      if (reason instanceof ApiError) setErrors(reason.body?.errors || null);
      toast(errorText(reason), 'error');
    }
  };

  return (
    <div className="layout">
      <section>
        <div className="section-head"><h2>Contas</h2></div>
        {accounts.length === 0 && <p className="empty">Nenhuma conta ainda. Crie a primeira ao lado.</p>}
        {accounts.map((account) => (
          <article className="account" key={account.id}>
            <div className="between">
              <div>
                <strong>{account.name}</strong>
                <div className="muted">{account.bank}</div>
              </div>
              <button type="button" className="ghost" onClick={() => remove(account.id)}>Excluir</button>
            </div>
            <p className={`amount ${account.balance < 0 ? 'expense' : ''}`}>{money(account.balance)}</p>
          </article>
        ))}
      </section>
      <form className="panel" onSubmit={submit}>
        <h2>Nova conta</h2>
        <Field label="Nome" name="name" errors={errors}>
          <input name="name" maxLength={100} required />
        </Field>
        <Field label="Banco" name="bank" errors={errors}>
          <input name="bank" maxLength={100} required />
        </Field>
        <p className="muted">O saldo começa em zero e muda com as transações.</p>
        <button className="primary" type="submit">Criar conta</button>
      </form>
    </div>
  );
}
