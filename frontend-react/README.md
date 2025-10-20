# Frontend React (JavaScript) per backend C#

Frontend minimale in **React + Vite** (senza TypeScript), pronto per collegarsi a un backend C#.
Il proxy dev inoltra automaticamente le richieste `'/api'` a `http://localhost:5100` (modificabile in `vite.config.js`).

## Avvio in sviluppo
```bash
npm install
npm run dev
```

- App in dev: `http://localhost:5173`
- Richieste a `/api/*` vanno al backend C# (porta 5100 di default).

## Build di produzione
```bash
npm run build
```
La cartella `dist/` può essere copiata dentro `wwwroot` del tuo progetto ASP.NET Core se vuoi servire il frontend dallo stesso host.

## Struttura
```
frontend-react/
├─ index.html
├─ vite.config.js
├─ package.json
├─ public/
└─ src/
   ├─ main.jsx
   ├─ App.jsx
   ├─ api.js
   └─ index.css
```

## Note
- Modifica l'endpoint in `App.jsx` o usa `api.js` per centralizzare le chiamate.
- Se il tuo backend non usa la porta 5100, cambia il `target` in `vite.config.js`.
