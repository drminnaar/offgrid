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
import { AppBarUserMenu } from './app-bar-user-menu';

const menuItems = [
  { name: 'Home', href: '/' },
  { name: 'Bike', href: '/products?types=Bike' },
  { name: 'Kayak', href: '/products?types=Kayak' },
  { name: 'Surfboard', href: '/products?types=Surfboard' },
];

export const AppBar = () => {
  return (
    <Navbar
      maxWidth='full'
      position='sticky'
      className='hidden sm:flex top-0 z-50 bg-background'
    >
      <NavbarContent justify='start' />
      <NavbarContent justify='center'>
        <AppBarBrand />
      </NavbarContent>
      <NavbarContent justify='end'>
        <AppBarUserMenu />
      </NavbarContent>
    </Navbar>
  );
};
