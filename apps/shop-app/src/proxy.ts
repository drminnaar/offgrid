import { NextResponse } from 'next/server';
import { auth } from '@/auth';

const protectedPrefixes = ['/profile', '/cart', '/checkout'] as const;

const isProtectedPath = (pathname: string) => {
  return protectedPrefixes.some(
    (prefix) => pathname === prefix || pathname.startsWith(`${prefix}/`)
  );
};

export const proxy = auth((request) => {
  const { nextUrl, auth: session } = request;

  if (!isProtectedPath(nextUrl.pathname) || session) {
    return NextResponse.next();
  }

  const signInUrl = new URL('/api/auth/signin', nextUrl.origin);
  signInUrl.searchParams.set('callbackUrl', `${nextUrl.pathname}${nextUrl.search}`);

  return NextResponse.redirect(signInUrl);
});

export const config = {
  matcher: ['/profile/:path*', '/cart/:path*', '/checkout/:path*'],
};
