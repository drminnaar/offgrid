import {
  CtaBannerSection,
  Footer,
  HeroSection,
  PopularCategoriesSection,
  ProductTypesSection,
  TestimonialsSection,
  TrustSection,
} from '@/components/home';

export default function Home() {
  return (
    <>
      <HeroSection />
      <ProductTypesSection />
      <PopularCategoriesSection />
      <TrustSection />
      <TestimonialsSection />
      <CtaBannerSection />
      <Footer />
    </>
  );
}
