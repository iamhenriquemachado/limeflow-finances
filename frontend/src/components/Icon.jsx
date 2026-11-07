const paths = {
  mark: 'M12 3c.4 2.2 1.2 3.6 3 4.6C13.2 8.6 12.4 10 12 12c-.4-2-1.2-3.4-3-4.4C10.8 6.6 11.6 5.2 12 3zm0 9a6 6 0 1 0 0 12 6 6 0 0 0 0-12z',
  panel: 'M4 5h7v6H4zm9 0h7v4h-7zM4 13h7v6H4zm9-2h7v8h-7z',
  transactions: 'M6 7h12M6 12h12M6 17h8',
  accounts: 'M4 8h16v10H4zM4 8l2.5-3h11L20 8',
  budgets: 'M5 19V9m5 10V5m5 14v-7m4 7V8',
  goals: 'M12 20s-6-3.6-6-8a3.5 3.5 0 0 1 6-2 3.5 3.5 0 0 1 6 2c0 4.4-6 8-6 8z',
  categories: 'M5 7h6l2 2h6v9H5z',
  leave: 'M10 6H6v12h4M10 12h8m0 0-3-3m3 3-3 3',
};

export function Icon({ name, size = 18 }) {
  if (name === 'mark') {
    return (
      <svg className="mark" width={size} height={size} viewBox="0 0 24 24" aria-hidden="true">
        <circle cx="12" cy="14.5" r="7" fill="#d6f25c" />
        <path d="M12 2.2c.6 2.6 1.6 4.1 3.8 5.1-2.2 1-3.2 2.5-3.8 5.1-.6-2.6-1.6-4.1-3.8-5.1 2.2-1 3.2-2.5 3.8-5.1z" fill="#1e6b43" />
      </svg>
    );
  }

  return (
    <svg width={size} height={size} viewBox="0 0 24 24" fill="none" aria-hidden="true">
      <path d={paths[name]} stroke="currentColor" strokeWidth="1.7" strokeLinejoin="round" strokeLinecap="round" />
    </svg>
  );
}
