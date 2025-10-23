import { useRef, useState, useLayoutEffect, useEffect } from "react";
import Hero1 from "../Hero1.jsx";
import Hero2 from "../Hero2.jsx";
import Hero3 from "../Hero3.jsx";
import Hero4 from "../Hero4.jsx";
import "./Carosello.css";

export default function Carosello() {
  const trackRef = useRef(null);
  const [index, setIndex] = useState(0);

  // Due pagine: ciascuna con due Hero
  const pages = [
    { id: 0, content: [<Hero1 key="h1" />, <Hero2 key="h2" />] },
    { id: 1, content: [<Hero3 key="h3" />, <Hero4 key="h4" />] },
  ];

  const getOffsetForPage = (i) => {
    const el = trackRef.current?.children?.[i];
    return el ? el.offsetLeft : 0;
  };

  const applyTransform = () => {
    const offset = getOffsetForPage(index);
    if (trackRef.current) {
      trackRef.current.style.transform = `translateX(-${offset}px)`;
    }
  };

  useLayoutEffect(() => {
    applyTransform();
  }, [index]);

  useEffect(() => {
    const onResize = () => applyTransform();
    window.addEventListener("resize", onResize);
    return () => window.removeEventListener("resize", onResize);
  }, []);

  // funzione che alterna tra 0 e 1
  const toggle = () => setIndex((prev) => (prev === 0 ? 1 : 0));

  return (
    <div className="carousel">
      <div className="viewport">
        <div className="track" ref={trackRef}>
          {pages.map((page) => (
            <div key={page.id} className="page">
              <div className="page-inner">
                <div className="col col-left">{page.content[0]}</div>
                <div className="col col-right">{page.content[1]}</div>
              </div>
            </div>
          ))}
        </div>
      </div>

      {/* ✅ Freccia che cambia direzione in base alla pagina */}
      <button
        className="btn single"
        onClick={toggle}
        aria-label="Cambia pagina"
      >
        {index === 0 ? "→" : "←"}
      </button>
    </div>
  );
}
