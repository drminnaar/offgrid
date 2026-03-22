workspace "Offgrid E-commerce System" "A modern distributed e-commerce platform for adventure gear." {

    model {
        guest = person "Guest Shopper" "A visitor who does not have an account but can browse products and purchase using guest checkout."
        customer = person "Registered Customer" "A registered user who logs in, browses products, places orders, submits reviews, earns rewards, and manages their account."
        staff = person "Staff Member" "Internal staff managing inventory, processing orders, and updating product catalogs."

        paymentGateway = softwareSystem "Payment Gateway" "Third-party payment processor (e.g., Stripe)." "External"
        shippingProvider = softwareSystem "Shipping Provider" "Third-party logistics API. Orchestrates the physical movement of products, encompassing shipping method selection, carrier integration, tracking, and returns management." "External"
        notificationService = softwareSystem "Notification Service" "Third-party notification service (e.g., Twilio, SendGrid)." "External"

        ecommerceSystem = softwareSystem "E-commerce System" "The primary e-commerce platform." {
            
            # --- Containers ---
            storefront = container "Web Storefront" "Provides e-commerce features via a web browser." "Next.js" "Web Browser"
            adminPortal = container "Admin Portal" "Internal management interface." "React" "Web Browser"
            
            storefrontApi = container "Storefront API" "Backend API for the web storefront." ".NET 10" {
                storefrontCustomerModule = component "Storefront Customer Module" "Handles customer-related operations." ".NET 10"
                storefrontProductModule = component "Storefront Product Module" "Handles product-related operations." ".NET 10"
            }

            adminPortalApi = container "Admin Portal API" "Backend API for the admin portal." ".NET 10" {
                adminCustomerModule = component "Admin Customer Module" "Handles customer management operations for staff." ".NET 10"
                adminProductModule = component "Admin Product Module" "Handles product management operations for staff." ".NET 10"
            }
            
            identityService = container "Identity Service" "Handles authentication and authorization." "Keycloak" {
                # --- Components (Level 3) ---
                storefrontRealm = component "Storefront Realm" "Realm for customer-facing applications." "Keycloak Realm"
                portalRealm = component "Portal Admin Realm" "Realm for internal staff applications." "Keycloak Realm"
            }

            pgDatabase = container "Database" "Relational data store." "PostgreSQL" {
                tags "Database,PostgreSQL"
            }
            
            searchIndex = container "Search Index" "High-performance product search." "Typesense" {
                tags "Database,Typesense"
            }
            
            messageBus = container "Message Bus" "Asynchronous event-driven communication." "RabbitMQ" {
                tags "RabbitMQ, Queue"
            }
            
            mongoDatabase = container "MongoDB" "NoSQL database for unstructured data." "MongoDB" {
                tags "Database,MongoDB"
            }

            customerOutboxProcessor = container "Customer Outbox Processor" "Background service that processes customer related outbox events for eventual consistency." ".NET 10"
            customerEventProcessor = container "Customer Event Processor" "Background service that processes customer related events from the message bus." ".NET 10"
            productSearchIndexer = container "Product Search Indexer" "Background service that updates the search index based on product catalog changes." ".NET 10"
        }

        # --- Relationships ---
        guest -> storefront "Browse catalog, use guest checkout"
        customer -> storefront "Log in, manage account, browse catalog, submit reviews, earn rewards, and checkout"
        staff -> adminPortal "Manage inventory, manage orders, manage discounts and sales"
        
        storefront -> storefrontApi "Makes API calls to" "HTTPS"
        storefrontApi -> storefrontRealm "Authenticates via" "OIDC"
        // storefrontApi -> storefrontProductModule "Find and filter products"
        // storefrontApi -> storefrontCustomerModule "Manage customer account"

        storefrontCustomerModule -> pgDatabase "Read and write customer and order data"
        storefrontProductModule -> searchIndex "Search products"

        storefrontApi -> paymentGateway "Processes payments via"
        storefrontApi -> shippingProvider "Ship products via"
        storefrontApi -> notificationService "Sends customer notifications via"

        customerOutboxProcessor -> pgDatabase "Reads pending customer outbox events"
        customerOutboxProcessor -> messageBus "Publishes CloudEvents to"

        customerEventProcessor -> messageBus "Consumes customer related events from"
        customerEventProcessor -> pgDatabase "Updates customer data in response to events"
        
        adminPortal -> adminPortalApi "Makes API calls to" "HTTPS"
        adminPortalApi -> portalRealm "Authenticates via" "OIDC"        
        // adminPortalApi -> storefrontCustomerModule "Manage customer accounts"
        // adminPortalApi -> storefrontProductModule "Manage product catalog and inventory"
        // adminPortalApi -> storefrontProductModule "Manage product search"
        adminCustomerModule -> pgDatabase "Read and write customer data"
        adminProductModule -> mongoDatabase "Read and write product catalog data"
        adminProductModule -> searchIndex "Updates product search index on product changes"

        adminPortalApi -> shippingProvider "Track shipments via"
        adminPortalApi -> notificationService "Sends staff notifications via"

        productSearchIndexer -> mongoDatabase "Reads and processes product catalog index jobs"
    }

    views {
        systemContext ecommerceSystem "SystemContext" {
            include *
            autoLayout
        }

        container ecommerceSystem "Containers" {
            include *
            autoLayout
        }

        component identityService "IdentityServiceComponents" {
            include *
            autoLayout
        }

        component storefrontApi "StorefrontApiComponents" {
            include *
            autoLayout
        }

        component adminPortalApi "AdminPortalApiComponents" {
            include *
            autoLayout
        }

        styles {
            element "Software System" {
                background #1168bd
                color #ffffff
            }
            element "Container" {
                background #438dd5
                color #ffffff
            }
            element "Component" {
                background #85bbf0
                color #000000
            }
            element "External" {
                background #999999
                color #ffffff
            }
            element "Database" {
                background #BBDEFB
                color #37474F
                shape Cylinder
            }
            element "PostgreSQL" {
                icon "https://cdn.simpleicons.org/postgresql/31648c"
            }
            element "MongoDB" {
                icon "https://cdn.simpleicons.org/mongodb/47A248"
            }
            element "RabbitMQ" {
                icon "https://cdn.simpleicons.org/rabbitmq/FF6600"
            }
            element "Typesense" {
                icon "https://avatars.githubusercontent.com/u/19822348"
            }
            element "Queue" {
                background #BBDEFB
                color #37474F
                shape pipe
            }
            element "Person" {
                shape person
            }
        }
    }
}