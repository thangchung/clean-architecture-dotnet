// import '../styles/globals.css'
import 'bootstrap/dist/css/bootstrap.css'
import 'react-bootstrap-table-next/dist/react-bootstrap-table2.css'
import 'react-bootstrap-table2-filter/dist/react-bootstrap-table2-filter.css'
import 'react-bootstrap-table2-paginator/dist/react-bootstrap-table2-paginator.css'
import 'nprogress/nprogress.css'; //styles of nprogress

import Router from 'next/router';
import NProgress from 'nprogress'; //nprogress module

Router.events.on('routeChangeStart', () => NProgress.start()); Router.events.on('routeChangeComplete', () => NProgress.done()); Router.events.on('routeChangeError', () => NProgress.done());

function MyApp({ Component, pageProps }) {
  return <Component {...pageProps} />
}

export default MyApp
