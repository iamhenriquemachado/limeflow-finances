import { NavLink, Navigate, Outlet } from 'react-router-dom';
import { session } from '../session.js';
import { Icon } from './Icon.jsx';

const links = [
  { to: '/painel', label: 'Painel', icon: 'panel' },
  { to: '/transacoes', label: 'Transações', icon: 'transactions' },
  { to: '/contas', label: 'Contas', icon: 'accounts' },
  { to: '/orcamentos', label: 'Orçamentos', icon: 'budgets' },
  { to: '/metas', label: 'Metas', icon: 'goals' },
  { to: '/categorias', label: 'Categorias', icon: 'categories' },
];

export function Shell() {
  if (!session.token) return <Navigate to="/entrar" replace />;

  const leave = () => {
    session.clear();
    location.hash = '#/entrar';
  };

  return (
    <div className="app">
      <header className="top">
        <NavLink to="/painel" className="brand">
          <Icon name="mark" size={22} />
          LimeFlow
        </NavLink>
        <nav className="nav">
          {links.map((link) => (
            <NavLink key={link.to} to={link.to}>
              <Icon name={link.icon} />
              <span>{link.label}</span>
            </NavLink>
          ))}
        </nav>
        <button type="button" className="leave" onClick={leave}>
          <Icon name="leave" />
          Sair
        </button>
      </header>
      <main className="stage">
        <Outlet />
      </main>
    </div>
  );
}
