import React from 'react';
import { Typography, Grid, Paper, Card, CardContent, Box } from '@mui/material';
import {
  Inventory2 as ProductsIcon,
  TrendingUp as TrendingUpIcon,
  Person as CustomersIcon,
  PersonAdd as NewCustomersIcon,
} from '@mui/icons-material';
import { RealmRole, useKeycloak } from '../../lib/auth/keycloak';

const StatCard = ({
  title,
  value,
  icon,
}: {
  title: string;
  value: string | number;
  icon: React.ReactNode;
}) => {
  return (
    <Card sx={{ height: '100%' }}>
      <CardContent>
        <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
          {icon}
          <Typography variant='h6' sx={{ ml: 2 }}>
            {title}
          </Typography>
        </Box>
        <Typography variant='h4' component='div'>
          {value}
        </Typography>
      </CardContent>
    </Card>
  );
};

export const DashboardPage = () => {
  const { keycloak } = useKeycloak();
  return (
    <Paper sx={{ p: 2 }}>
      <Typography variant='h4'>Dashboard</Typography>
      <Box>
        {/* Stats Row */}
        <Grid container spacing={3} sx={{ mb: 4 }}>
          {keycloak?.hasRealmRole(RealmRole.ProductManager) && (
            <Grid size={{ xs: 12, sm: 6, md: 3 }}>
              <StatCard
                title='Total Products'
                value='1,234'
                icon={<ProductsIcon color='primary' fontSize='large' />}
              />
            </Grid>
          )}
          {keycloak?.hasRealmRole(RealmRole.CustomerManager) && (
            <>
              <Grid size={{ xs: 12, sm: 6, md: 3 }}>
                <StatCard
                  title='Total Customers'
                  value='48'
                  icon={<CustomersIcon color='secondary' fontSize='large' />}
                />
              </Grid>
              <Grid size={{ xs: 12, sm: 6, md: 3 }}>
                <StatCard
                  title='New Customers'
                  value='3'
                  icon={<NewCustomersIcon color='warning' fontSize='large' />}
                />
              </Grid>
              <Grid size={{ xs: 12, sm: 6, md: 3 }}>
                <StatCard
                  title='Active Customers'
                  value='12%'
                  icon={<TrendingUpIcon color='success' fontSize='large' />}
                />
              </Grid>
            </>
          )}
        </Grid>

        {/* Additional Sections */}
        {keycloak?.hasRealmRole(RealmRole.ProductManager) && (
          <Grid container spacing={3}>
            <Grid size={{ xs: 12, md: 8 }}>
              <Paper sx={{ p: 3, height: 400 }}>
                <Typography variant='h6' gutterBottom>
                  Top Products
                </Typography>
                <Box
                  sx={{
                    bgcolor: 'grey.200',
                    height: '90%',
                    borderRadius: 1,
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                  }}
                >
                  <Typography color='text.secondary'>
                    List or table of top products goes here...
                  </Typography>
                </Box>
              </Paper>
            </Grid>

            {/* Quick Actions or Recent Products */}
            <Grid size={{ xs: 12, md: 4 }}>
              <Paper sx={{ p: 3, height: 400 }}>
                <Typography variant='h6' gutterBottom>
                  Recent Products
                </Typography>
                <Box
                  sx={{
                    bgcolor: 'grey.200',
                    height: '90%',
                    borderRadius: 1,
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                  }}
                >
                  <Typography color='text.secondary'>
                    List or table of recent products goes here...
                  </Typography>
                </Box>
              </Paper>
            </Grid>
          </Grid>
        )}
      </Box>
    </Paper>
  );
};
