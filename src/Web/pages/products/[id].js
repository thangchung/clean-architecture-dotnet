import { useRouter } from "next/router";
import { useForm } from "react-hook-form";

import MainLayout from "../../layout/MainLayout";
import HeadDefault from "../../layout/head/HeadDefault";

// https://react-hook-form.com/get-started
const Product = () => {
  const router = useRouter();
  const { id } = router.query;

  const { register, handleSubmit, watch, errors } = useForm();
  const onSubmit = (data) => console.log(data);

  return (
    <>
      <HeadDefault
        title="Product Detail page | eCommerce"
        description="Product detail page of eCommerce."
      />
      <MainLayout>
        <form onSubmit={handleSubmit(onSubmit)}>
          <input
            name="id"
            defaultValue={id}
            ref={register({ required: true })}
          />
          {errors.id && <span>This field is required</span>}

          <input type="submit" />
        </form>
      </MainLayout>
    </>
  );
};

export default Product;
