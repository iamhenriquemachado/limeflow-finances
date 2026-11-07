import { createContext, useContext, useEffect, useState } from 'react';
import { fieldError } from '../api.js';

const ToastContext = createContext(() => {});
const ConfirmContext = createContext(async () => false);

export function useToast() {
  return useContext(ToastContext);
}

export function useConfirm() {
  return useContext(ConfirmContext);
}

export function Providers({ children }) {
  const [toast, setToast] = useState(null);
  const [confirm, setConfirm] = useState(null);

  useEffect(() => {
    if (!toast) return undefined;
    const timer = setTimeout(() => setToast(null), 3500);
    return () => clearTimeout(timer);
  }, [toast]);

  useEffect(() => {
    if (!confirm) return undefined;
    const onKey = (event) => {
      if (event.key === 'Escape') {
        confirm.resolve(false);
        setConfirm(null);
      }
    };
    window.addEventListener('keydown', onKey);
    return () => window.removeEventListener('keydown', onKey);
  }, [confirm]);

  const show = (message, kind = 'ok') => setToast({ message, kind });
  const ask = (message) => new Promise((resolve) => setConfirm({ message, resolve }));
  const close = (value) => {
    confirm?.resolve(value);
    setConfirm(null);
  };

  return (
    <ToastContext.Provider value={show}>
      <ConfirmContext.Provider value={ask}>
        {children}
        <div id="toast" role="status" className={toast ? 'show' : ''} data-kind={toast?.kind || 'ok'}>
          {toast?.message}
        </div>
        {confirm && (
          <div className="scrim" role="dialog" aria-modal="true">
            <div className="sheet">
              <p>{confirm.message}</p>
              <div className="actions">
                <button type="button" className="quiet" onClick={() => close(false)}>Cancelar</button>
                <button type="button" className="primary" onClick={() => close(true)}>Excluir</button>
              </div>
            </div>
          </div>
        )}
      </ConfirmContext.Provider>
    </ToastContext.Provider>
  );
}

export function Field({ label, name, errors, children }) {
  const message = fieldError(errors, name);
  return (
    <label>
      <span>{label}</span>
      {children}
      {message ? <small className="err">{message}</small> : null}
    </label>
  );
}

export function Meter({ value, tone = '' }) {
  const [width, setWidth] = useState(0);
  useEffect(() => {
    const frame = requestAnimationFrame(() => setWidth(Math.min(value || 0, 100)));
    return () => cancelAnimationFrame(frame);
  }, [value]);
  return (
    <div className={`meter ${tone}`}>
      <i style={{ width: `${width}%` }} />
    </div>
  );
}

export function Loading() {
  return <p className="empty">Carregando…</p>;
}

export function Failure({ message }) {
  return <p className="empty">{message}</p>;
}
