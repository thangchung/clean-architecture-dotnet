/* eslint-disable no-unused-vars */
/** npm packages */
import React, { Component } from 'react';
import Head from 'next/head';
import { THEME } from '../../constants/vars';

function HeadDefault({
  title,
  description,
  keyword,
  ogTitle,
  ogDescription,
  ogImageUrl,
  ogImageAlt,
  ogUrl,
}) {
  return (
    <Head>
      <title>
        {title} | {THEME.title}
      </title>

      <link rel="icon" href="/favicon.ico" />

      {/* META SEO */}
      <meta name="title" content={title} />
      <meta name="description" content={description} />
      <meta name="keywords" content={keyword} />

      {/* META OG */}
      <meta property="og:title" content={ogTitle} />
      <meta property="og:description" content={ogDescription} />
      <meta property="og:url" content={ogUrl} />
      <meta property="og:image" content={ogImageUrl} />
      <meta property="og:image:url" content={ogImageUrl} />
      <meta property="og:image:alt" content={ogImageAlt} />
      <meta property="og:image:type" content="image/jpg" />
      <meta property="og:image:width" content="1200" />
      <meta property="og:image:height" content="628" />
    </Head>
  );
}

export default HeadDefault;
