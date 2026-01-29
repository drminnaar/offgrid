'use client';

// packages
import {
  Navbar,
  NavbarContent,
  NavbarItem,
  NavbarMenuToggle,
  NavbarMenu,
  NavbarMenuItem,
  Link,
} from '@heroui/react';
import { useState } from 'react';
import { usePathname } from 'next/navigation';

// app components
import { AppBarBrand } from './app-bar-brand';

const menuItems = [
  { name: 'Home', href: '/' },
  { name: 'Bike', href: '/bike' },
  { name: 'Kayak', href: '/kayak' },
  { name: 'Surfboard', href: '/surfboard' },
  { name: 'Snowboard', href: '/snowboard' },
];

export const AppBar = () => {
  const pathname = usePathname();
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  return (
    <>
      <Navbar maxWidth='full' className='hidden sm:flex'>
        <NavbarContent justify='start' />
        <NavbarContent justify='center'>
          <AppBarBrand />
        </NavbarContent>
        <NavbarContent justify='end'>
          <div>{/* Placeholder for right-aligned content */}</div>
        </NavbarContent>
      </Navbar>

      <Navbar onMenuOpenChange={setIsMenuOpen} isBordered maxWidth='full'>
        <NavbarContent>
          <NavbarMenuToggle
            aria-label={isMenuOpen ? 'Close menu' : 'Open menu'}
            className='sm:hidden'
          />
        </NavbarContent>

        <NavbarContent justify='center'>
          <AppBarBrand className='sm:hidden' />
        </NavbarContent>

        <NavbarContent className='hidden sm:flex gap-4' justify='center'>
          {menuItems.map((item, index) => (
            <NavbarItem
              key={`${item.name.toLowerCase()}-${index}`}
              isActive={pathname === item.href}
            >
              <Link
                color='foreground'
                href={item.href}
                className={`hover:text-primary-200 ${
                  pathname === item.href
                    ? 'underline underline-offset-8 text-primary-500'
                    : ''
                }`}
              >
                {item.name}
              </Link>
            </NavbarItem>
          ))}
        </NavbarContent>
        <NavbarContent justify='end'>
          <div className='sm:hidden'>
            {/* Placeholder for right-aligned content on mobile */}
          </div>
        </NavbarContent>

        <NavbarMenu>
          {menuItems.map((item, index) => (
            <NavbarMenuItem
              key={`${item.name.toLowerCase()}-${index}`}
              isActive={pathname === item.href}
              className={`${pathname === item.href ? 'bg-primary/10' : ''}`}
            >
              <Link
                color='foreground'
                className='w-full hover:text-primary-400'
                href={item.href}
                size='lg'
              >
                {item.name}
              </Link>
            </NavbarMenuItem>
          ))}
        </NavbarMenu>
      </Navbar>
    </>
  );
};
