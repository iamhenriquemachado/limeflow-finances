import { HashRouter, Navigate, Route, Routes } from 'react-router-dom';
import { Providers } from './components/Ui.jsx';
import { Shell } from './components/Shell.jsx';
import { Accounts } from './pages/Accounts.jsx';
import { Budgets } from './pages/Budgets.jsx';
import { Categories } from './pages/Categories.jsx';
import { Dashboard } from './pages/Dashboard.jsx';
import { Goals } from './pages/Goals.jsx';
import { Login } from './pages/Login.jsx';
import { Transactions } from './pages/Transactions.jsx';

export function App() {
  return (
    <Providers>
      <HashRouter>
        <Routes>
          <Route path="/entrar" element={<Login />} />
          <Route element={<Shell />}>
            <Route index element={<Navigate to="/painel" replace />} />
            <Route path="/painel" element={<Dashboard />} />
            <Route path="/transacoes" element={<Transactions />} />
            <Route path="/contas" element={<Accounts />} />
            <Route path="/orcamentos" element={<Budgets />} />
            <Route path="/metas" element={<Goals />} />
            <Route path="/categorias" element={<Categories />} />
          </Route>
          <Route path="*" element={<Navigate to="/painel" replace />} />
        </Routes>
      </HashRouter>
    </Providers>
  );
}
