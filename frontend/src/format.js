export const money = (value) =>
  new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value ?? 0);

export const formatDate = (value) => new Date(value).toLocaleDateString('pt-BR');

export const toIso = (value) => (value ? new Date(value).toISOString() : undefined);

export const tomorrow = () => new Date(Date.now() + 864e5).toISOString().slice(0, 10);

export const localNow = () =>
  new Date(Date.now() - new Date().getTimezoneOffset() * 6e4).toISOString().slice(0, 16);

export const startOfMonth = () => {
  const date = new Date();
  return new Date(date.getFullYear(), date.getMonth(), 1).toISOString();
};

export const months = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];

export const periodLabels = { Weekly: 'Semanal', Monthly: 'Mensal', Yearly: 'Anual' };

export const typeLabels = { Income: 'Receitas', Expense: 'Despesas' };

export const icons = ['🎯', '🏠', '✈️', '🚗', '🎓', '🛟'];
