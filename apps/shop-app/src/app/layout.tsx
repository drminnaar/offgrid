import type { Metadata } from 'next';
import './globals.css';
import { APP_DESCRIPTION, APP_NAME } from './env-constants';
import { RootAppProvider } from '@/components/providers';
import { AppBar } from '@/components/ui/layout';

export const metadata: Metadata = {
  title: APP_NAME,
  description: APP_DESCRIPTION,
  icons: {
    icon: [
      { url: '/favicon.ico', sizes: 'any' },
      { url: '/favicon-96x96.png', sizes: '96x96', type: 'image/png' },
    ],
    apple: '/apple-touch-icon.png',
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang='en' suppressHydrationWarning={true}>
      <body>
        <RootAppProvider>
          <AppBar />
          <main>{children}</main>
        </RootAppProvider>
      </body>
    </html>
  );
}
