import React from "react";
import { Layout, Menu } from "antd";

import Link from "next/link";
import { atom, useAtom } from "jotai";

const { Header } = Layout;
const selectNavAtom = atom(["/"]);

function NavBar() {
  const [selectNav, setSelectNav] = useAtom(selectNavAtom);

  const handleClick = (e) => {
    setSelectNav(() => e.key);
  };

  return (
    <>
      <Header
        id="components-layout-demo-fixed"
        style={{ position: "fixed", zIndex: 9999, width: "100%" }}
      >
        <div className="logo">eCommerce</div>
        <Menu
          theme="dark"
          mode="horizontal"
          selectedKeys={[selectNav]}
          onClick={handleClick}
        >
          <Menu.Item key="/">
            <Link href="/">Home</Link>
          </Menu.Item>
          <Menu.Item key="/products">
            <Link href="/products">Products</Link>
          </Menu.Item>
        </Menu>
      </Header>
    </>
  );
}

export default NavBar;
