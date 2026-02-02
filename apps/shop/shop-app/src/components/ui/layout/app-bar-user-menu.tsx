import { useRouter } from 'next/navigation';
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
  const router = useRouter();
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
            Sign Up
          </DropdownItem>
        </DropdownMenu>
      </Dropdown>
    );
  }

  const handleSignout = () => {
    signOut({
      redirectTo: '/',
    });
  };

  const handleViewProfile = () => {
    router.push('/profile');
  };

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
          key='email'
          className='h-14 gap-2'
          textValue={session.user.email || 'User Email'}
          onPress={handleViewProfile}
        >
          <p className='font-semibold'>Signed in as</p>
          <p className='font-semibold'>{session.user.email}</p>
        </DropdownItem>
        <DropdownItem
          key='profile'
          className='h-14 gap-2'
          textValue='View Profile'
          onPress={handleViewProfile}
        >
          View Profile
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
