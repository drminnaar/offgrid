'use client';

// react packages
import { useEffect, useState } from 'react';
import Link from 'next/link';

// heroui packages
import { Button, Chip } from '@heroui/react';

// custom icons
import { IconArrow, IconWave } from '../icons';

const HERO_SLIDES = [
  {
    tag: 'New Season Drop',
    title: 'Ride the\nHorizon',
    sub: 'Kayaks engineered for explorers who chase the horizon, from tranquil lakes to open seas and wild rivers.',
    cta: 'Shop Kayaks',
    href: '/products?types=Kayak',
    accent: '#00C2A8',
    bg: 'linear-gradient(135deg, #0A1628 0%, #0D2B45 50%, #0A3D3A 100%)',
    emoji: '🛶',
  },
  {
    tag: 'Best Sellers',
    title: 'Catch Every\nWave',
    sub: 'Surfboards shaped for performance, from dawn patrol to evening glass-off.',
    cta: 'Shop Surfboards',
    href: '/products?types=Surfboard',
    accent: '#FF6B35',
    bg: 'linear-gradient(135deg, #1A0A00 0%, #3D1A00 50%, #2D0F1F 100%)',
    emoji: '🏄',
  },
  {
    tag: 'Trail Ready',
    title: 'Own Every\nTerrain',
    sub: 'Mountain, gravel, electric — bikes built for riders who define their own path.',
    cta: 'Shop Bikes',
    href: '/products?types=Bike',
    accent: '#7EE87E',
    bg: 'linear-gradient(135deg, #061208 0%, #0D2B1A 50%, #1A2B08 100%)',
    emoji: '🚵',
  },
];

export const HeroSection = () => {
  const [slide, setSlide] = useState(0);
  const current = HERO_SLIDES[slide];
  useEffect(() => {
    const timer = setInterval(
      () => setSlide((s) => (s + 1) % HERO_SLIDES.length),
      5000,
    );
    return () => clearInterval(timer);
  }, []);
  return (
    <section
      style={{
        minHeight: '90vh',
        background: current.bg,
        transition: 'background 0.8s ease',
        display: 'flex',
        alignItems: 'center',
        overflow: 'hidden',
        position: 'relative',
      }}
    >
      {/* Decorative grid */}
      <div
        style={{
          position: 'absolute',
          inset: 0,
          opacity: 0.04,
          backgroundImage:
            'linear-gradient(rgba(255,255,255,0.5) 1px, transparent 1px), linear-gradient(90deg, rgba(255,255,255,0.5) 1px, transparent 1px)',
          backgroundSize: '60px 60px',
        }}
      />

      {/* Glowing orb */}
      <div
        style={{
          position: 'absolute',
          right: '10%',
          top: '20%',
          width: 500,
          height: 500,
          borderRadius: '50%',
          background: `radial-gradient(circle, ${current.accent}25 0%, transparent 70%)`,
          transition: 'background 0.8s ease',
          pointerEvents: 'none',
        }}
      />

      {/* Big emoji decoration */}
      <div
        style={{
          position: 'absolute',
          right: '8%',
          top: '50%',
          transform: 'translateY(-50%)',
          fontSize: 200,
          opacity: 0.12,
          pointerEvents: 'none',
          userSelect: 'none',
          transition: 'all 0.5s ease',
          filter: 'blur(2px)',
        }}
      >
        {current.emoji}
      </div>

      <div
        style={{
          maxWidth: 1280,
          margin: '0 auto',
          padding: '0 32px',
          width: '100%',
          paddingTop: 100,
        }}
      >
        <div key={slide} className='hero-slide-enter' style={{ maxWidth: 680 }}>
          <Chip
            style={{
              background: `${current.accent}20`,
              color: current.accent,
              border: `1px solid ${current.accent}50`,
              fontWeight: 700,
              fontSize: 12,
              letterSpacing: '0.1em',
              textTransform: 'uppercase',
              marginBottom: 24,
            }}
          >
            <span
              className='dot-pulse'
              style={{
                width: 6,
                height: 6,
                borderRadius: '50%',
                background: current.accent,
                display: 'inline-block',
                marginRight: 8,
              }}
            />
            {current.tag}
          </Chip>

          <h1
            style={{
              fontFamily: "'Playfair Display', serif",
              fontSize: 'clamp(52px, 8vw, 96px)',
              fontWeight: 900,
              color: '#FFFFFF',
              lineHeight: 1.0,
              letterSpacing: '-0.03em',
              marginBottom: 28,
              whiteSpace: 'pre-line',
            }}
          >
            {current.title}
          </h1>

          <p
            style={{
              fontSize: 'clamp(16px, 2vw, 19px)',
              color: 'rgba(255,255,255,0.65)',
              maxWidth: 460,
              lineHeight: 1.7,
              marginBottom: 44,
              fontFamily: "'DM Sans', sans-serif",
            }}
          >
            {current.sub}
          </p>

          <div style={{ display: 'flex', gap: 14, flexWrap: 'wrap' }}>
            <Button
              as={Link}
              href={current.href}
              size='lg'
              endContent={<IconArrow />}
              style={{
                background: current.accent,
                color: '#0D1117',
                fontWeight: 700,
                fontSize: 15,
                letterSpacing: '0.02em',
                padding: '0 28px',
                height: 52,
                boxShadow: `0 8px 32px ${current.accent}50`,
                transition: 'all 0.2s',
              }}
            >
              {current.cta}
            </Button>
          </div>
        </div>

        {/* Slide dots */}
        <div style={{ display: 'flex', gap: 8, marginTop: 56 }}>
          {HERO_SLIDES.map((s, i) => (
            <button
              key={i}
              onClick={() => setSlide(i)}
              style={{
                width: i === slide ? 32 : 8,
                height: 8,
                borderRadius: 100,
                background:
                  i === slide ? current.accent : 'rgba(255,255,255,0.25)',
                border: 'none',
                cursor: 'pointer',
                transition: 'all 0.3s ease',
                padding: 0,
              }}
            />
          ))}
        </div>
      </div>

      {/* Wave divider */}
      <div
        style={{
          position: 'absolute',
          bottom: -2,
          left: 0,
          right: 0,
          color: '#F8F9FA',
        }}
      >
        <IconWave />
      </div>
    </section>
  );
};
