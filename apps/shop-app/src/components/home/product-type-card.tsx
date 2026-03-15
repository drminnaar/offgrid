'use client';

// heroui components
import { Chip } from '@heroui/react';

// custom types
import { ProductType } from './types';

type Props = {
  product: ProductType;
  isActive: boolean;
  onClick: () => void;
};

export const ProductTypeCard = ({ product, isActive, onClick }: Props) => {
  return (
    <div
      onClick={onClick}
      style={{
        background: isActive ? '#0D1117' : '#FFFFFF',
        border: `2px solid ${isActive ? product.accent : '#E8EAED'}`,
        borderRadius: 20,
        padding: '28px 24px',
        cursor: 'pointer',
        transition: 'all 0.3s cubic-bezier(0.34, 1.56, 0.64, 1)',
        transform: isActive ? 'scale(1.02)' : 'scale(1)',
        boxShadow: isActive
          ? `0 24px 64px ${product.accent}30`
          : '0 2px 12px rgba(0,0,0,0.05)',
        position: 'relative',
        overflow: 'hidden',
      }}
    >
      {isActive && (
        <div
          style={{
            position: 'absolute',
            inset: 0,
            opacity: 0.05,
            backgroundImage:
              'radial-gradient(circle at 70% 30%, white 0%, transparent 60%)',
          }}
        />
      )}
      <div style={{ fontSize: 44, marginBottom: 16 }}>{product.icon}</div>
      <Chip
        size='sm'
        style={{
          background: `${product.accent}20`,
          color: product.accent,
          fontSize: 11,
          fontWeight: 700,
          letterSpacing: '0.06em',
          marginBottom: 10,
          border: `1px solid ${product.accent}40`,
        }}
      >
        {product.count}
      </Chip>
      <h3
        style={{
          fontFamily: "'Playfair Display', serif",
          fontSize: 26,
          fontWeight: 800,
          color: isActive ? '#FFFFFF' : '#0D1117',
          marginBottom: 6,
          letterSpacing: '-0.02em',
        }}
      >
        {product.label}
      </h3>
      <p
        style={{
          fontSize: 13,
          fontWeight: 600,
          color: product.accent,
          marginBottom: 12,
          fontFamily: "'DM Sans', sans-serif",
          letterSpacing: '0.04em',
          textTransform: 'uppercase',
        }}
      >
        {product.tagline}
      </p>
      <p
        style={{
          fontSize: 14,
          color: isActive ? 'rgba(255,255,255,0.65)' : '#666',
          marginBottom: 20,
          lineHeight: 1.6,
          fontFamily: "'DM Sans', sans-serif",
        }}
      >
        {product.desc}
      </p>
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: 6 }}>
        {product.categories.map((category) => (
          <span
            key={category}
            style={{
              fontSize: 12,
              padding: '4px 10px',
              borderRadius: 100,
              background: isActive
                ? 'rgba(255,255,255,0.1)'
                : product.lightAccent,
              color: isActive ? 'rgba(255,255,255,0.8)' : product.accent,
              fontWeight: 600,
              fontFamily: "'DM Sans', sans-serif",
              border: `1px solid ${isActive ? 'rgba(255,255,255,0.15)' : product.accent + '30'}`,
            }}
          >
            {category}
          </span>
        ))}
      </div>
    </div>
  );
};
