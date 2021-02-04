import { useRouter } from "next/router";
import { useForm } from "react-hook-form";

// https://react-hook-form.com/get-started
const Product = () => {
  const router = useRouter();
  const { id } = router.query;

  const { register, handleSubmit, watch, errors } = useForm();
  const onSubmit = (data) => console.log(data);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <input name="id" defaultValue={id} ref={register({ required: true })} />
      {errors.id && <span>This field is required</span>}

      <input type="submit" />
    </form>
  );
};

export default Product;
