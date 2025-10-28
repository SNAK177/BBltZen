const BASE = import.meta.env.VITE_API_BASE;

function authHeaders(){
  const t = localStorage.getItem('token');
  return t ? { Authorization: `Bearer ${t}` } : {};
}

export async function apiGet(p){
  const r = await fetch(`${BASE}${p}`, { headers:{...authHeaders()} });
  if(!r.ok) throw new Error(await r.text());
  return r.json();
}
export async function apiPost(p,b){
  const r = await fetch(`${BASE}${p}`, {
    method:'POST',
    headers:{'Content-Type':'application/json',...authHeaders()},
    body:JSON.stringify(b)
  });
  if(!r.ok) throw new Error(await r.text());
  return r.json();
}
export async function apiPut(p,b){
  const r = await fetch(`${BASE}${p}`, {
    method:'PUT',
    headers:{'Content-Type':'application/json',...authHeaders()},
    body:JSON.stringify(b)
  });
  if(!r.ok) throw new Error(await r.text());
  return r.json();
}
