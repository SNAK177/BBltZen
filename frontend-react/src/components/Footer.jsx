import React from 'react'
import {FaFacebook, FaInstagram, FaTwitter} from 'react-icons/fa'
import './Footer.css'

export default function Footer() {
    const socialLinks = [
        {
            label: "Facebook",
            icon: FaFacebook,
            href: "https://www.facebook.com"
        },
        {
            label: "Instagram",
            icon: FaInstagram,
            href: "https://www.instagram.com"
        },
        {
            label: "Twitter",
            icon: FaTwitter,
            href: "https://www.twitter.com"
        }
    ]
    return (
        <footer className="bbt-footer">
            <div className="bbt-footer-container">

                {/* Sezione Loghi Social */}
                <div className="bbt-social-links">
                    {socialLinks.map((link) => {
                        const Icon = link.icon;
                        return (
                            <a
                                key={link.label}
                                href={link.url}
                                target="_blank"
                                rel="noopener noreferrer"
                                aria-label={link.label}
                                className="bbt-social-icon"
                            >
                                <Icon size={24}/>
                            </a>
                        );
                    })}
                </div>

                {/* Sezione Copyright */}
                <div className="bbt-copyright">
                    <p>
                        © {new Date().getFullYear()} Bubble Freddy. Tutti i diritti riservati.
                    </p>
                    <p className="bbt-theme-notice">
                        Tema fan-made.
                    </p>
                </div>
            </div>
        </footer>
    );
}
