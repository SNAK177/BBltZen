import { useEffect, useState } from 'react'

export default function App() {
  const [data, setData] = useState(null)
  const [error, setError] = useState('')

  useEffect(() => {
    fetch('/api/hello') // Adatta al tuo endpoint reale
      .then(res => {
        if (!res.ok) throw new Error('Errore chiamando il backend')
        return res.json()
      })
      .then(setData)
      .catch(e => setError(e.message))
  }, [])

  return (
    <div style={{ fontFamily: 'system-ui, sans-serif', padding: 24, maxWidth: 720, margin: '0 auto' }}>
      <h1>Frontend React (JS)</h1>
      <p>Pronto per lavorare con il tuo backend C#.</p>

      {error && <p role="alert">❌ {error}</p>}
      {data ? (
        <div style={{ padding: 16, border: '1px solid #ddd', borderRadius: 8 }}>
          <p><strong>Risposta backend:</strong> {data.message ?? JSON.stringify(data)}</p>
          {'time' in data && <p><strong>UTC time:</strong> {new Date(data.time).toLocaleString()}</p>}
        </div>
      ) : (
        !error && <p>Carico i dati da <code>/api/hello</code>…</p>
      )}
    </div>
  )
}
