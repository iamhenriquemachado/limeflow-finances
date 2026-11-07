import { session } from './session.js';

export class ApiError extends Error {
  constructor(status, body) {
    super(body?.detail || 'Erro');
    this.status = status;
    this.body = body;
  }
}

const fallback = {
  400: 'Confira os campos destacados.',
  404: 'Item não encontrado.',
  500: 'Algo deu errado no servidor. Tente novamente.',
};

export function errorText(error) {
  if (!(error instanceof ApiError)) return error.message;
  return error.body?.detail || fallback[error.status] || 'Não foi possível concluir a ação.';
}

export function fieldError(errors, name) {
  if (!errors) return null;
  const key = Object.keys(errors).find((item) => item.toLowerCase() === name.toLowerCase());
  return key ? errors[key][0] : null;
}

// #region agent log
function debugLog(hypothesisId, message, data) {
  fetch('http://127.0.0.1:7854/ingest/fc977345-e431-465b-b338-b28f9bf5db7a',{method:'POST',headers:{'Content-Type':'application/json','X-Debug-Session-Id':'b6e95b'},body:JSON.stringify({sessionId:'b6e95b',hypothesisId,location:'api.js',message,data,timestamp:Date.now()})}).catch(()=>{});
}
// #endregion

export async function api(path, { method = 'GET', body, query } = {}) {
  const params = new URLSearchParams(
    Object.entries(query || {}).filter(([, value]) => value !== '' && value != null),
  );
  const url = path + (params.toString() ? `?${params}` : '');
  const headers = {};
  if (body) headers['Content-Type'] = 'application/json';
  if (session.token) headers.Authorization = `Bearer ${session.token}`;

  // #region agent log
  debugLog('H2', 'request', { method, path, keys: body && typeof body === 'object' ? Object.keys(body) : [], hasToken: Boolean(session.token) });
  // #endregion

  let response;
  try {
    response = await fetch(url, { method, headers, body: body && JSON.stringify(body) });
  } catch {
    // #region agent log
    debugLog('H3', 'network', { method, path });
    // #endregion
    throw new Error('Não foi possível conectar à API.');
  }

  const json = response.status === 204 ? null : await response.json().catch(() => null);
  if (!response.ok) {
    const authCall = path.includes('/sessions') || path.endsWith('/password');
    if (response.status === 401 && !authCall) {
      session.clear();
      location.hash = '#/entrar';
    }
    // #region agent log
    debugLog(response.status === 401 ? 'H4' : 'H1', 'error', {
      method,
      path,
      status: response.status,
      title: json?.title || json?.detail || null,
      errorKeys: json?.errors ? Object.keys(json.errors) : [],
      clearedSession: response.status === 401 && !authCall,
    });
    // #endregion
    throw new ApiError(response.status, json);
  }
  // #region agent log
  debugLog('H5', 'ok', { method, path, status: response.status });
  // #endregion
  return json;
}
