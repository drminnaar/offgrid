import {
  Card,
  CardMedia,
  Stack,
  Tooltip,
  IconButton,
  Avatar,
} from '@mui/material';
import { toPlaceholderImage } from '../utils/to-placeholder-image';
import { useState } from 'react';

type ProductImageGalleryProps = {
  images: { url: string; colorHex?: string; isPrimary?: boolean }[];
};

export const ProductImageGallery = ({ images }: ProductImageGalleryProps) => {
  const primaryImage = images.find((img) => img.isPrimary) || images[0];
  const [currentImage, setCurrentImage] = useState(primaryImage);
  return (
    <>
      <Card elevation={3}>
        <CardMedia
          component='img'
          image={toPlaceholderImage(currentImage?.url || images[0]?.url)}
          alt={currentImage?.url}
          sx={{
            height: 400,
            objectFit: 'scale-down',
            bgcolor: currentImage?.colorHex || '#f0f0f0',
          }}
        />
      </Card>

      <Stack
        direction='row'
        spacing={1}
        mt={2}
        justifyContent='center'
        sx={{ flexWrap: 'wrap', gap: 1 }}
      >
        {images.length > 0 &&
          images.map((image, idx) => (
            <Tooltip key={idx} title={`Gallery Image ${idx + 1}`}>
              <IconButton onClick={() => setCurrentImage(image)}>
                <Avatar
                  variant='rounded'
                  src={toPlaceholderImage(image.url)}
                  sx={{
                    bgcolor: image.colorHex || '#000000',
                    width: 80,
                    height: 80,
                    border:
                      currentImage === image ? '3px solid #1976d2' : 'none',
                  }}
                />
              </IconButton>
            </Tooltip>
          ))}
      </Stack>
    </>
  );
};
