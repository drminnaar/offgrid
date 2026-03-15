'use client';

export const TrustSection = () => {
  return (
    <section style={{ background: '#0D1117', padding: '56px 32px' }}>
      <div
        style={{
          maxWidth: 1280,
          margin: '0 auto',
          display: 'grid',
          gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))',
          gap: 40,
        }}
      >
        {[
          { icon: '🚚', title: 'Free Shipping', sub: 'On orders over $200' },
          {
            icon: '🔄',
            title: '60-Day Returns',
            sub: 'Hassle-free guarantee',
          },
          { icon: '🛡️', title: '2-Year Warranty', sub: 'On all products' },
          {
            icon: '🏆',
            title: 'Expert Staff',
            sub: 'Adventurers, not salespeople',
          },
        ].map((item) => (
          <div
            key={item.title}
            style={{ display: 'flex', gap: 16, alignItems: 'center' }}
          >
            <div style={{ fontSize: 32, flexShrink: 0 }}>{item.icon}</div>
            <div>
              <h4
                style={{
                  color: '#fff',
                  fontWeight: 700,
                  fontSize: 15,
                  marginBottom: 2,
                }}
              >
                {item.title}
              </h4>
              <p
                style={{
                  color: 'rgba(255,255,255,0.45)',
                  fontSize: 13,
                }}
              >
                {item.sub}
              </p>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
};
