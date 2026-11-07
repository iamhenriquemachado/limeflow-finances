import { useState } from 'react';
import { Navigate } from 'react-router-dom';
import { ApiError, api, errorText } from '../api.js';
import { Icon } from '../components/Icon.jsx';
import { Field, useToast } from '../components/Ui.jsx';
import { session } from '../session.js';

export function Login() {
  const toast = useToast();
  const [creating, setCreating] = useState(false);
  const [errors, setErrors] = useState(null);
  if (session.token) return <Navigate to="/painel" replace />;

  const submit = async (event) => {
    event.preventDefault();
    const form = new FormData(event.currentTarget);
    const body = Object.fromEntries(form);
    setErrors(null);
    // #region agent log
    fetch('http://127.0.0.1:7854/ingest/fc977345-e431-465b-b338-b28f9bf5db7a',{method:'POST',headers:{'Content-Type':'application/json','X-Debug-Session-Id':'b6e95b'},body:JSON.stringify({sessionId:'b6e95b',hypothesisId:'H2',location:'Login.jsx:submit',message:'auth submit',data:{creating,fields:Object.keys(body),hasName:Boolean(body.name)},timestamp:Date.now()})}).catch(()=>{});
    // #endregion
    try {
      if (creating) await api('/api/v1/users', { method: 'POST', body });
      session.save(await api('/api/v1/sessions', {
        method: 'POST',
        body: { email: body.email, password: body.password },
      }));
      location.hash = '#/painel';
    } catch (error) {
      if (error instanceof ApiError) setErrors(error.body?.errors || null);
      toast(errorText(error), 'error');
    }
  };

  return (
    <section className="auth">
      <Icon name="mark" size={36} />
      <h1>LimeFlow</h1>
      <p className="muted">Suas finanças pessoais em um só lugar.</p>
      <form className="panel" onSubmit={submit}>
        <div className="seg">
          <button type="button" className={creating ? 'quiet' : 'primary'} onClick={() => setCreating(false)}>Entrar</button>
          <button type="button" className={creating ? 'primary' : 'quiet'} onClick={() => setCreating(true)}>Criar conta</button>
        </div>
        {creating && (
          <Field label="Nome" name="name" errors={errors}>
            <input name="name" autoComplete="name" required minLength={3} maxLength={100} />
          </Field>
        )}
        <Field label="E-mail" name="email" errors={errors}>
          <input name="email" type="email" autoComplete="email" required />
        </Field>
        <Field label="Senha" name="password" errors={errors}>
          <input name="password" type="password" autoComplete={creating ? 'new-password' : 'current-password'} required />
        </Field>
        <button className="primary" type="submit">{creating ? 'Criar conta' : 'Entrar'}</button>
      </form>
    </section>
  );
}
