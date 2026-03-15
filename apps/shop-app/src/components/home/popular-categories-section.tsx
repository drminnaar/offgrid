'use client';

// react packages
import { useState } from 'react';

// heroui components
import { Button, Chip } from '@heroui/react';

// custom types
import { ProductCategory } from './types';
import { ProductCategoryCard } from './product-category-card';

const POPULAR_CATEGORIES: ProductCategory[] = [
  {
    name: 'Sea Kayaks',
    type: 'kayaks',
    badge: '🔥 Hot',
    price: 'From $1,299',
    accent: '#00C2A8',
    img: '🌊',
  },
  {
    name: 'Fishing Kayaks',
    type: 'kayaks',
    badge: 'Top Pick',
    price: 'From $899',
    accent: '#00A896',
    img: '🎣',
  },
  {
    name: 'Shortboards',
    type: 'surfboards',
    badge: 'New Shapes',
    price: 'From $649',
    accent: '#FF6B35',
    img: '🏄',
  },
  {
    name: 'Longboards',
    type: 'surfboards',
    badge: 'Classic',
    price: 'From $799',
    accent: '#FF8C55',
    img: '🌅',
  },
  {
    name: 'Mountain Bikes',
    type: 'bikes',
    badge: '🔥 Hot',
    price: 'From $1,499',
    accent: '#7EE87E',
    img: '⛰️',
  },
  {
    name: 'Electric Bikes',
    type: 'bikes',
    badge: 'Trending',
    price: 'From $2,199',
    accent: '#5DC85D',
    img: '⚡',
  },
  {
    name: 'Gravel Bikes',
    type: 'bikes',
    badge: 'Staff Pick',
    price: 'From $1,199',
    accent: '#9EF09E',
    img: '🌿',
  },
  {
    name: 'Fish Surfboards',
    type: 'surfboards',
    badge: 'Fun Factor',
    price: 'From $549',
    accent: '#FF5520',
    img: '🐟',
  },
];

export const PopularCategoriesSection = () => {
  const [filterType, setFilterType] = useState('all');

  const filteredCategories =
    filterType === 'all'
      ? POPULAR_CATEGORIES
      : POPULAR_CATEGORIES.filter((c) => c.type === filterType);

  return (
    <section style={{ background: '#FFFFFF', padding: '96px 32px' }}>
      <div style={{ maxWidth: 1280, margin: '0 auto' }}>
        <div
          style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'flex-end',
            marginBottom: 48,
            flexWrap: 'wrap',
            gap: 20,
          }}
        >
          <div>
            <Chip
              style={{
                background: '#FF6B3515',
                color: '#FF6B35',
                border: '1px solid #FF6B3530',
                fontWeight: 700,
                fontSize: 11,
                letterSpacing: '0.1em',
                marginBottom: 12,
              }}
            >
              MOST POPULAR
            </Chip>
            <h2
              style={{
                fontFamily: "'Playfair Display', serif",
                fontSize: 'clamp(32px, 4vw, 48px)',
                fontWeight: 800,
                color: '#0D1117',
                letterSpacing: '-0.03em',
                lineHeight: 1.1,
              }}
            >
              Fan Favourites
            </h2>
          </div>
          <div style={{ display: 'flex', gap: 8 }}>
            {[
              { label: 'All', val: 'all' },
              { label: 'Kayaks', val: 'kayaks' },
              { label: 'Surfboards', val: 'surfboards' },
              { label: 'Bikes', val: 'bikes' },
            ].map((f) => (
              <Button
                key={f.val}
                onPress={() => setFilterType(f.val)}
                style={{
                  padding: '8px 16px',
                  borderRadius: 100,
                  border: '1.5px solid',
                  borderColor: filterType === f.val ? '#0D1117' : '#E0E0E0',
                  background: filterType === f.val ? '#0D1117' : 'transparent',
                  color: filterType === f.val ? '#fff' : '#555',
                  fontFamily: "'DM Sans', sans-serif",
                  fontWeight: 600,
                  fontSize: 13,
                  cursor: 'pointer',
                  transition: 'all 0.2s ease',
                }}
              >
                {f.label}
              </Button>
            ))}
          </div>
        </div>

        <div
          style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fill, minmax(220px, 1fr))',
            gap: 16,
          }}
        >
          {filteredCategories.map((category, i) => (
            <ProductCategoryCard
              key={category.name + filterType}
              category={category}
              index={i}
            />
          ))}
        </div>
      </div>
    </section>
  );
};
