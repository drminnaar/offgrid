'use client';

import React from 'react';
import { HeroUIProvider } from '@heroui/react';
import { useRouter } from 'next/navigation';
import { ThemeProvider } from 'next-themes';
import { SessionProvider } from 'next-auth/react';

export const RootAppProvider = ({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) => {
  const router = useRouter();
  return (
    <ThemeProvider
      attribute='class'
      defaultTheme='light'
      disableTransitionOnChange
      enableSystem={false}
    >
      <HeroUIProvider navigate={router.push} className='flex flex-col h-full'>
        <SessionProvider>{children}</SessionProvider>
      </HeroUIProvider>
    </ThemeProvider>
  );
};
