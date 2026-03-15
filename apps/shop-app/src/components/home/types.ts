export type ProductType = {
  id: string;
  label: string;
  tagline: string;
  desc: string;
  accent: string;
  lightAccent: string;
  icon: string;
  count: string;
  categories: string[];
};

export type ProductCategory = {
  name: string;
  type: string;
  badge: string;
  price: string;
  accent: string;
  img: string;
};