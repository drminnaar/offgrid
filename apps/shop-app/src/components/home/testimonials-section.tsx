'use client';

import { Chip, Card, CardBody, Divider, Avatar } from '@heroui/react';
import { IconStar } from '../icons/icon-star';

const TESTIMONIALS = [
  {
    name: 'Alex P.',
    avatar: 'A',
    text: 'The sea kayak I bought handles swells like it was born in them. Absolutely phenomenal quality.',
    stars: 5,
    product: 'Sea Kayak Pro X1',
  },
  {
    name: 'Maya L.',
    avatar: 'M',
    text: 'My gravel bike transformed my commute into something I actually look forward to. Zero regrets.',
    stars: 5,
    product: 'Gravel Titan 3',
  },
  {
    name: 'Sam K.',
    avatar: 'S',
    text: "Been surfing 20 years and this longboard is the smoothest ride I've ever had. Game changer.",
    stars: 5,
    product: 'Classic Logger 9\'6"',
  },
];

const StarRating = ({ count }: { count: number }) => {
  return (
    <span
      style={{
        display: 'flex',
        gap: 2,
        alignItems: 'center',
        color: '#F5A623',
      }}
    >
      {Array.from({ length: count }).map((_, i) => (
        <IconStar key={i} />
      ))}
    </span>
  );
};

export const TestimonialsSection = () => {
  return (
    <section style={{ background: '#F8F9FA', padding: '96px 32px' }}>
      <div style={{ maxWidth: 1280, margin: '0 auto' }}>
        <div style={{ textAlign: 'center', marginBottom: 56 }}>
          <Chip
            style={{
              background: '#7EE87E20',
              color: '#3D9E3D',
              border: '1px solid #7EE87E40',
              fontWeight: 700,
              fontSize: 11,
              letterSpacing: '0.1em',
              marginBottom: 12,
            }}
          >
            STORIES FROM THE TRAIL
          </Chip>
          <h2
            style={{
              fontFamily: "'Playfair Display', serif",
              fontSize: 'clamp(32px, 4vw, 48px)',
              fontWeight: 800,
              color: '#0D1117',
              letterSpacing: '-0.03em',
            }}
          >
            Real Adventures. Real Gear.
          </h2>
        </div>
        <div
          style={{
            display: 'grid',
            gridTemplateColumns: 'repeat(auto-fit, minmax(280px, 1fr))',
            gap: 20,
          }}
        >
          {TESTIMONIALS.map((t, i) => (
            <Card
              key={i}
              style={{
                background: '#fff',
                border: '1.5px solid #F0F0F0',
                boxShadow: '0 4px 24px rgba(0,0,0,0.05)',
              }}
            >
              <CardBody style={{ padding: 28 }}>
                <StarRating count={t.stars} />
                <p
                  style={{
                    fontSize: 15,
                    color: '#333',
                    lineHeight: 1.7,
                    margin: '16px 0',
                    fontFamily: "'DM Sans', sans-serif",
                  }}
                >
                  "{t.text}"
                </p>
                <Divider style={{ margin: '16px 0' }} />
                <div style={{ display: 'flex', alignItems: 'center', gap: 12 }}>
                  <Avatar
                    name={t.avatar}
                    style={{
                      background: '#0D1117',
                      color: '#fff',
                      fontWeight: 700,
                    }}
                    size='sm'
                  />
                  <div>
                    <p
                      style={{
                        fontWeight: 700,
                        fontSize: 14,
                        color: '#0D1117',
                        fontFamily: "'DM Sans', sans-serif",
                      }}
                    >
                      {t.name}
                    </p>
                    <p
                      style={{
                        fontSize: 12,
                        color: '#999',
                        fontFamily: "'DM Sans', sans-serif",
                      }}
                    >
                      {t.product}
                    </p>
                  </div>
                </div>
              </CardBody>
            </Card>
          ))}
        </div>
      </div>
    </section>
  );
};
