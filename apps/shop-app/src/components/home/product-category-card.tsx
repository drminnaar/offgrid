'use client';

// react packages
import { useState } from 'react';

// heroui components
import { Card, CardBody, Chip, CardFooter, Button } from '@heroui/react';

// custom icons
import { IconArrow } from '../icons';

// custom types
import { ProductCategory } from './types';

type Props = {
  category: ProductCategory;
  index: number;
};

export const ProductCategoryCard = ({ category, index }: Props) => {
  const [hovered, setHovered] = useState(false);
  return (
    <Card
      onMouseEnter={() => setHovered(true)}
      onMouseLeave={() => setHovered(false)}
      style={{
        background: hovered ? '#FFFFFF' : '#F8F9FA',
        border: `2px solid ${hovered ? category.accent : 'transparent'}`,
        transition: 'all 0.25s ease',
        transform: hovered ? 'translateY(-6px)' : 'none',
        boxShadow: hovered
          ? `0 20px 60px ${category.accent}30`
          : '0 2px 12px rgba(0,0,0,0.06)',
        cursor: 'pointer',
        animationDelay: `${index * 80}ms`,
      }}
      className='fade-up'
    >
      <CardBody style={{ padding: '24px' }}>
        <div
          style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'flex-start',
            marginBottom: 16,
          }}
        >
          <div style={{ fontSize: 40, lineHeight: 1 }}>{category.img}</div>
          <Chip
            size='sm'
            style={{
              background: `${category.accent}18`,
              color: category.accent,
              fontWeight: 700,
              fontSize: 11,
              letterSpacing: '0.04em',
              border: `1px solid ${category.accent}40`,
            }}
          >
            {category.badge}
          </Chip>
        </div>
        <h3
          style={{
            fontFamily: "'Playfair Display', serif",
            fontSize: 20,
            fontWeight: 700,
            color: '#0D1117',
            marginBottom: 6,
            letterSpacing: '-0.01em',
          }}
        >
          {category.name}
        </h3>
        <p
          style={{
            fontSize: 15,
            fontWeight: 700,
            color: category.accent,
            fontFamily: "'DM Sans', sans-serif",
          }}
        >
          {category.price}
        </p>
      </CardBody>
      <CardFooter style={{ padding: '0 24px 20px', paddingTop: 0 }}>
        <Button
          size='sm'
          endContent={<IconArrow />}
          style={{
            background: hovered ? category.accent : 'transparent',
            color: hovered ? '#fff' : category.accent,
            border: `1.5px solid ${category.accent}`,
            fontWeight: 600,
            fontSize: 13,
            transition: 'all 0.2s ease',
            fontFamily: "'DM Sans', sans-serif",
            letterSpacing: '0.02em',
          }}
        >
          Explore
        </Button>
      </CardFooter>
    </Card>
  );
};
