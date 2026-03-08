// custom components
import { AppPage } from '../../layout';
import { ProductIndexJobSummary } from '../product-index-job-summary';

export const ProductIndexingPage = () => {
  return (
    <AppPage title='Product Indexing'>
      <ProductIndexJobSummary />
    </AppPage>
  );
};
