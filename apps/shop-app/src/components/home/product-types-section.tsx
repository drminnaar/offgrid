'use client';

// react packages
import { useState } from 'react';

// heroui components
import { Chip } from '@heroui/react';

// custom components
import { ProductTypeCard } from './product-type-card';

// types
import { ProductType } from './types';

const PRODUCT_TYPES: ProductType[] = [
  {
    id: 'kayaks',
    label: 'Kayaks',
    tagline: 'Paddle Your Story',
    desc: 'From glassy lakes to open ocean. Find your hull.',
    accent: '#00C2A8',
    lightAccent: '#E0FAF7',
    icon: '🛶',
    count: '48 models',
    categories: ['Fishing', 'Sea', 'Whitewater', 'Touring', 'Sit-on-Top'],
  },
  {
    id: 'surfboards',
    label: 'Surfboards',
    tagline: 'Read the Ocean',
    desc: 'Shortboards, longboards, fish, and foilboards.',
    accent: '#FF6B35',
    lightAccent: '#FFF0E8',
    icon: '🏄',
    count: '62 models',
    categories: ['Shortboard', 'Longboard', 'Fish', 'Funboard', 'Foil'],
  },
  {
    id: 'bikes',
    label: 'Bikes',
    tagline: 'Every Trail, Every Road',
    desc: 'Electric, mountain, gravel, and everything between.',
    accent: '#7EE87E',
    lightAccent: '#EDFAED',
    icon: '🚵',
    count: '55 models',
    categories: ['Mountain', 'Electric', 'Gravel', 'Road', 'Fat Tire'],
  },
];

export const ProductTypesSection = () => {
  const [activeProduct, setActiveProduct] = useState(0);
  return (
    <section style={{ background: '#F8F9FA', padding: '96px 32px' }}>
      <div style={{ maxWidth: 1280, margin: '0 auto' }}>
        <div style={{ textAlign: 'center', marginBottom: 56 }}>
          <Chip
            style={{
              background: '#0D1117',
              color: '#fff',
              fontWeight: 700,
              fontSize: 11,
              letterSpacing: '0.1em',
              marginBottom: 16,
            }}
          >
            OUR COLLECTIONS
          </Chip>
          <h2
            style={{
              fontFamily: "'Playfair Display', serif",
              fontSize: 'clamp(36px, 5vw, 56px)',
              fontWeight: 800,
              color: '#0D1117',
              letterSpacing: '-0.03em',
              lineHeight: 1.1,
              marginBottom: 16,
            }}
          >
            Three Worlds.
            <br />
            One Store.
          </h2>
          <p
            style={{
              fontSize: 17,
              color: '#666',
              maxWidth: 480,
              margin: '0 auto',
              lineHeight: 1.7,
              fontFamily: "'DM Sans', sans-serif",
            }}
          >
            Curated gear for those who find meaning in motion — on water, waves,
            or wheels.
          </p>
        </div>

        <div
          style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))',
            gap: 20,
          }}
        >
          {PRODUCT_TYPES.map((product, i) => (
            <ProductTypeCard
              key={product.id}
              product={product}
              isActive={activeProduct === i}
              onClick={() => setActiveProduct(i)}
            />
          ))}
        </div>
      </div>
    </section>
  );
};
