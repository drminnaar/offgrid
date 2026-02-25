// custom components
import { AppPage } from '../../layout';
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
