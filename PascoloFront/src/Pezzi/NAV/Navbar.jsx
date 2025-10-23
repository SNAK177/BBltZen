import "./Navbar.css";
import logo from "../../Img/Logo_Progetto.png";

function Navbar() {
  return (
    <nav>
      <div className="brand">
        <img src={logo} alt="Logo" className="logo" />
       
      </div>

      <ul className="menu">
        <li><a href="#">Home</a></li>
        <li><a href="#">Chi siamo</a></li>
        <li><a href="#">Contatti</a></li>
      </ul>
    </nav>
  );
}

export default Navbar;
