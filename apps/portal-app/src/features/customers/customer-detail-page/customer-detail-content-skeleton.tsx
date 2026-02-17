import {
  Box,
  Card,
  CardContent,
  CardHeader,
  Skeleton,
  Chip,
} from '@mui/material';

export const CustomerDetailContentSkeleton = () => (
  <Box className='max-w-xl mt-8'>
    <Card>
      <CardHeader
        title={
          <Box className='flex justify-between items-center'>
            <Box className='flex items-center gap-2'>
              <Skeleton
                variant='rectangular'
                width={60}
                height={32}
                sx={{ borderRadius: 1 }}
              >
                <Chip label='' />
              </Skeleton>
              <Skeleton variant='text' width={200} height={32} />
            </Box>
            <Skeleton
              variant='rectangular'
              width={100}
              height={36}
              sx={{ borderRadius: 1 }}
            />
          </Box>
        }
        className='bg-gray-50 border-b border-gray-200'
      />
      <CardContent>
        <Box className='flex flex-col gap-4'>
          <Skeleton variant='text' width='80%' height={28} />
          <Skeleton variant='text' width='60%' height={28} />
          <Skeleton variant='text' width='70%' height={28} />
          <Skeleton variant='text' width='50%' height={28} />
          <Skeleton
            variant='rectangular'
            width='100%'
            height={2}
            sx={{ my: 2 }}
          />
          <Skeleton variant='text' width='40%' height={24} />
          <Skeleton variant='text' width='40%' height={24} />
          <Skeleton variant='text' width='40%' height={24} />
        </Box>
      </CardContent>
    </Card>
  </Box>
);
