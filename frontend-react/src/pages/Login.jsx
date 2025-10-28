import React from 'react'
import './Login.css'

export default function Login(){
  return (
    <section className="fnaf-hero">
      <div className="scanlines" aria-hidden="true" />
      <div className="noise" aria-hidden="true" />

      <div className="fnaf-wrap">
        <h1 className="fnaf-title">
          <span className="glitch" data-text="BUBBLE FREDDY">BUBBLE FREDDY</span>
          <span className="fnaf-break">LOGIN</span>
        </h1>
        <p className="fnaf-subtitle">from 12:00 am at 1:00 am — from 6:00 pm at 10:00 pm</p>
        <div className="fnaf-cta">
          <button className="btn btn-red">Accedi</button>
          <button className="btn btn-outline">Iscriviti</button>
        </div>
      </div>
    </section>
  )
}
