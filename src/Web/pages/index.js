import MainLayout from '../layout/MainLayout';
import HeadDefault from '../layout/head/HeadDefault';

export default function Home() {
  return (
    <>
        <HeadDefault
          title="Home | eCommerce"
          description="Home page of eCommerce"
        />
        <MainLayout>
        </MainLayout>
      </>
  )
}
