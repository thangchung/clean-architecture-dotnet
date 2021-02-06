import React from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  NavbarText,
} from 'reactstrap';

import { atom, useAtom } from "jotai";

function NavBar() {
  const toggleAtom = atom(false);
  const [toggle, setToggle] = useAtom(toggleAtom);

  return (
    <div>
      <Navbar color="light" light expand="md">
        <NavbarBrand href="/"><h5><b>eCommerce</b></h5></NavbarBrand>
        <NavbarToggler onClick={() => {setToggle(!toggle)}} />
        <Collapse isOpen={toggle} navbar>
          <Nav className="mr-auto" navbar>
            <NavItem>
              <NavLink href="/products">Products</NavLink>
            </NavItem>
          </Nav>
          <NavbarText>Thang Chung</NavbarText>
        </Collapse>
      </Navbar>
    </div>
  );
}

export default NavBar;
