'use client';

import { Button, Input } from '@heroui/react';

export const CtaBannerSection = () => {
  return (
    <section
      style={{
        background: '#0D1117',
        padding: '96px 32px',
        position: 'relative',
        overflow: 'hidden',
      }}
    >
      <div
        style={{
          position: 'absolute',
          inset: 0,
          opacity: 0.06,
          backgroundImage:
            'radial-gradient(circle at 20% 50%, #00C2A8 0%, transparent 40%), radial-gradient(circle at 80% 50%, #FF6B35 0%, transparent 40%)',
        }}
      />
      <div
        style={{
          maxWidth: 800,
          margin: '0 auto',
          textAlign: 'center',
          position: 'relative',
        }}
      >
        <div style={{ fontSize: 56, marginBottom: 20 }}>🌊 🏄 🚵</div>
        <h2
          style={{
            fontFamily: "'Playfair Display', serif",
            fontSize: 'clamp(36px, 5vw, 60px)',
            fontWeight: 900,
            color: '#fff',
            letterSpacing: '-0.03em',
            lineHeight: 1.1,
            marginBottom: 20,
          }}
        >
          Your Next Adventure
          <br />
          Starts Here
        </h2>
        <p
          style={{
            fontSize: 17,
            color: 'rgba(255,255,255,0.6)',
            marginBottom: 40,
            lineHeight: 1.7,
            fontFamily: "'DM Sans', sans-serif",
          }}
        >
          Join 12,000+ adventurers. Subscribe for new arrivals, exclusive drops,
          and trail reports.
        </p>
        <div
          style={{
            display: 'flex',
            gap: 12,
            justifyContent: 'center',
            flexWrap: 'wrap',
          }}
        >
          <Input
            placeholder='your@email.com'
            size='lg'
            style={{
              maxWidth: 320,
              background: 'rgba(255,255,255,0.08)',
              color: '#fff',
              border: '1.5px solid rgba(255,255,255,0.15)',
              borderRadius: 12,
            }}
          />
          <Button
            size='lg'
            style={{
              background: 'linear-gradient(135deg, #00C2A8, #FF6B35)',
              color: '#fff',
              fontWeight: 700,
              fontFamily: "'DM Sans', sans-serif",
              fontSize: 15,
              height: 48,
              padding: '0 28px',
              letterSpacing: '0.02em',
            }}
          >
            Join the Crew
          </Button>
        </div>
      </div>
    </section>
  );
};
