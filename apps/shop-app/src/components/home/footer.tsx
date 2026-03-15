'use client';

import { Divider } from '@heroui/react';
import { MountainSnow } from 'lucide-react';

export const Footer = () => {
  return (
    <footer
      style={{
        background: '#080C12',
        padding: '56px 32px 32px',
        color: 'rgba(255,255,255,0.5)',
      }}
    >
      <div style={{ maxWidth: 1280, margin: '0 auto' }}>
        <div
          style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(160px, 1fr))',
            gap: 40,
            marginBottom: 48,
          }}
        >
          <div>
            <div
              style={{
                display: 'flex',
                alignItems: 'center',
                gap: 10,
                marginBottom: 16,
              }}
            >
              <div
                style={{
                  width: 32,
                  height: 32,
                  borderRadius: 8,
                  background: 'linear-gradient(135deg, #00C2A8, #FF6B35)',
                  display: 'flex',
                  alignItems: 'center',
                  justifyContent: 'center',
                  fontSize: 16,
                }}
              >
                <MountainSnow />
              </div>
              <span
                style={{
                  fontFamily: "'Playfair Display', serif",
                  fontWeight: 800,
                  color: '#fff',
                  fontSize: 17,
                }}
              >
                Offgrid
              </span>
            </div>
            <p style={{ fontSize: 13, lineHeight: 1.8 }}>
              Gear for those who live outside.
            </p>
          </div>
          {[
            {
              title: 'Shop',
              links: ['Kayaks', 'Surfboards', 'Bikes', 'Sale', 'Gift Cards'],
            },
            {
              title: 'Company',
              links: ['About', 'Journal', 'Careers', 'Press'],
            },
            {
              title: 'Support',
              links: ['FAQ', 'Shipping', 'Returns', 'Contact'],
            },
          ].map((col) => (
            <div key={col.title}>
              <h5
                style={{
                  color: '#fff',
                  fontWeight: 700,
                  fontSize: 13,
                  letterSpacing: '0.08em',
                  textTransform: 'uppercase',
                  marginBottom: 16,
                  fontFamily: "'DM Sans', sans-serif",
                }}
              >
                {col.title}
              </h5>
              {col.links.map((link) => (
                <a
                  key={link}
                  href='#'
                  style={{
                    display: 'block',
                    fontSize: 13,
                    marginBottom: 8,
                    color: 'rgba(255,255,255,0.45)',
                    textDecoration: 'none',
                    fontFamily: "'DM Sans', sans-serif",
                    transition: 'color 0.2s',
                  }}
                  onMouseEnter={(e) => {
                    (e.target as HTMLElement).style.color = '#fff';
                  }}
                  onMouseLeave={(e) => {
                    (e.target as HTMLElement).style.color =
                      'rgba(255,255,255,0.45)';
                  }}
                >
                  {link}
                </a>
              ))}
            </div>
          ))}
        </div>
        <Divider
          style={{ background: 'rgba(255,255,255,0.08)', marginBottom: 24 }}
        />
        <div
          style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            flexWrap: 'wrap',
            gap: 12,
          }}
        >
          <p style={{ fontSize: 12, fontFamily: "'DM Sans', sans-serif" }}>
            © 2026 Offgrid. All rights reserved.
          </p>
          <p style={{ fontSize: 12, fontFamily: "'DM Sans', sans-serif" }}>
            Privacy · Terms · Cookies
          </p>
        </div>
      </div>
    </footer>
  );
};
