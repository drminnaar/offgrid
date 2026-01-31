import { signIn, signOut, useSession } from 'next-auth/react';
import {
  Dropdown,
  DropdownTrigger,
  Avatar,
  DropdownMenu,
  DropdownItem,
  AvatarIcon,
} from '@heroui/react';

export const AppBarUserMenu = () => {
  const { data: session } = useSession();

  const handleSignin = () => {
    signIn('keycloak', { redirectTo: '/' }, { prompt: 'login' });
  };

  const handleSignup = () => {
    signIn('keycloak', {
      callbackUrl: '/',
      kc_action: 'register',
    });
  };

  const handleSignout = () => {
    signOut({
      redirectTo: '/',
    });
  };

  if (!session?.user) {
    return (
      <Dropdown placement='bottom-end'>
        <DropdownTrigger>
          <Avatar
            isBordered
            as='button'
            className='transition-transform cursor-pointer'
            color='default'
            size='sm'
            icon={<AvatarIcon />}
          />
        </DropdownTrigger>
        <DropdownMenu aria-label='Profile Actions' variant='flat'>
          <DropdownItem
            key='signin'
            color='primary'
            onPress={handleSignin}
            textValue='Sign In'
          >
            Sign In
          </DropdownItem>
          <DropdownItem
            key='signup'
            color='secondary'
            textValue='Sign Up'
            onPress={handleSignup}
          >
            {/* <a href={registerUrl}>Sign Up</a> */}
            Sign Up
          </DropdownItem>
        </DropdownMenu>
      </Dropdown>
    );
  }

  return (
    <Dropdown placement='bottom-end'>
      <DropdownTrigger>
        <Avatar
          isBordered
          as='button'
          className='transition-transform cursor-pointer'
          color='secondary'
          name={session.user.name || session.user.email || 'User'}
          showFallback
          size='sm'
          src={session.user.image || ''}
        />
      </DropdownTrigger>
      <DropdownMenu aria-label='Profile Actions' variant='flat'>
        <DropdownItem
          key='profile'
          className='h-14 gap-2'
          textValue={session.user.email || 'User Email'}
        >
          <p className='font-semibold'>Signed in as</p>
          <p className='font-semibold'>{session.user.email}</p>
        </DropdownItem>
        <DropdownItem
          key='logout'
          color='danger'
          onPress={handleSignout}
          textValue='Sign Out'
        >
          Sign Out
        </DropdownItem>
      </DropdownMenu>
    </Dropdown>
  );
};
