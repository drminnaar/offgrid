'use client';

// packages
import {
  Navbar,
  NavbarContent,
  NavbarMenuToggle,
  NavbarMenu,
  NavbarMenuItem,
  Link,
} from '@heroui/react';
import { useState } from 'react';
import { usePathname, useRouter } from 'next/navigation';
import { signIn, signOut, useSession } from 'next-auth/react';

// app components
import { AppBarBrand } from './app-bar-brand';
import { AppBarUserMenu } from './app-bar-user-menu';

const menuItems = [
  { name: 'Home', href: '/' },
  { name: 'Bike', href: '/products?types=Bike' },
  { name: 'Kayak', href: '/products?types=Kayak' },
  { name: 'Surfboard', href: '/products?types=Surfboard' },
];

export const AppBar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const pathname = usePathname();
  const router = useRouter();
  const { data: session } = useSession();

  const closeMenu = () => setIsMenuOpen(false);

  const handleRoute = (href: string) => {
    router.push(href);
    closeMenu();
  };

  const handleSignin = () => {
    closeMenu();
    signIn('keycloak', { redirectTo: '/' }, { prompt: 'login' });
  };

  const handleSignup = () => {
    closeMenu();
    signIn('keycloak', {
      callbackUrl: '/',
      kc_action: 'register',
    });
  };

  const handleSignout = () => {
    closeMenu();
    signOut({ redirectTo: '/' });
  };

  return (
    <Navbar
      maxWidth='full'
      position='sticky'
      className='top-0 z-50 bg-background'
      isMenuOpen={isMenuOpen}
      onMenuOpenChange={setIsMenuOpen}
    >
      <NavbarContent justify='start' className='sm:hidden'>
        <NavbarMenuToggle
          aria-label={isMenuOpen ? 'Close menu' : 'Open menu'}
        />
      </NavbarContent>
      <NavbarContent justify='center'>
        <AppBarBrand />
      </NavbarContent>
      <NavbarContent justify='end' className='hidden sm:flex'>
        <AppBarUserMenu />
      </NavbarContent>

      <NavbarMenu>
        {menuItems.map((item) => {
          const isActive =
            pathname === item.href || pathname.startsWith(`${item.href}?`);

          return (
            <NavbarMenuItem key={item.href} isActive={isActive}>
              <Link
                color={isActive ? 'primary' : 'foreground'}
                size='lg'
                href={item.href}
                onPress={closeMenu}
              >
                {item.name}
              </Link>
            </NavbarMenuItem>
          );
        })}

        <NavbarMenuItem className='mt-2'>
          <div className='h-px w-full bg-divider' />
        </NavbarMenuItem>

        {session?.user ? (
          <>
            <NavbarMenuItem>
              <Link
                color='foreground'
                size='lg'
                onPress={() => handleRoute('/profile')}
              >
                Profile
              </Link>
            </NavbarMenuItem>
            <NavbarMenuItem>
              <Link color='danger' size='lg' onPress={handleSignout}>
                Sign Out
              </Link>
            </NavbarMenuItem>
          </>
        ) : (
          <>
            <NavbarMenuItem>
              <Link color='primary' size='lg' onPress={handleSignin}>
                Sign In
              </Link>
            </NavbarMenuItem>
            <NavbarMenuItem>
              <Link color='secondary' size='lg' onPress={handleSignup}>
                Sign Up
              </Link>
            </NavbarMenuItem>
          </>
        )}
      </NavbarMenu>
    </Navbar>
  );
};
