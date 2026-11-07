const tokenKey = 'lf.token';
const expiresKey = 'lf.exp';

export const session = {
  get token() {
    const expiresAt = Number(sessionStorage.getItem(expiresKey));
    if (expiresAt && Date.now() > expiresAt) this.clear();
    return sessionStorage.getItem(tokenKey);
  },
  save({ accessToken, expiresIn }) {
    sessionStorage.setItem(tokenKey, accessToken);
    sessionStorage.setItem(expiresKey, String(Date.now() + expiresIn * 1000));
  },
  clear() {
    sessionStorage.removeItem(tokenKey);
    sessionStorage.removeItem(expiresKey);
  },
};
