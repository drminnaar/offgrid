// packages
import { AppPage } from '../../layout';

// custom components
import { CustomerPageContent } from './customer-page-content';

export const CustomerPage = () => {
  return (
    <>
      <AppPage title='Customers'>
        <CustomerPageContent />
      </AppPage>
    </>
  );
};
