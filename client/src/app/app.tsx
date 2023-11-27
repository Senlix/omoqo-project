import { Layout } from 'antd';
import { Header, Content, Footer } from 'antd/es/layout/layout';
import styles from './app.module.scss';
import { Outlet } from 'react-router-dom';

export function App() {
  return (
    <Layout className={styles.layout}>
      <Header className={styles.header}>
        <div className={styles.logo}>
          <span>Omoqo Project</span>
        </div>
      </Header>

      <Content>
        <div className={styles.contentWrapper}>
          <Outlet />
        </div>
      </Content>
      <Footer className={styles.footer}>
        Omoqo Project ©2023 Created by Fellipe Lima
      </Footer>
    </Layout>
  );
}

export default App;
