var __index = {"config":{"lang":["en"],"separator":"[\\s\\u200b\\-_,:!=\\[\\]()\"`/]+|\\.(?!\\d)|&[lg]t;|(?!\\b)(?=[A-Z][a-z])","pipeline":["stopWordFilter"]},"docs":[{"location":"index.html","title":"Welcome to Integrated System Documentation","text":"<p>Welcome to the user guide for the Legend Motor Company Integrated System.</p> <p>This comprehensive software application is designed to streamline the operations of Legend Motor Company, offering a centralized platform for managing various business aspects. The system includes modules for order management, spare part management, stock management, user management, and invoice management.</p> <p>This user guide provides detailed instructions on how to effectively use the Legend Motor Company Integrated System. The guide is divided into several sections, each dedicated to a specific aspect of the system, ensuring you have all the necessary information to perform various tasks efficiently.</p>"},{"location":"index.html#guide-sections","title":"Guide Sections","text":"<ul> <li>Installation: Learn how to install and set up the system on your local machine.</li> <li>Order Management: Learn how to handle orders efficiently within the system.</li> <li>Spare Part Management: Manage spare parts inventory and details with ease.</li> <li>Stock Management: Keep track of stock levels and movements accurately.</li> <li>User Management: Manage user accounts, roles, and permissions effectively.</li> <li>Invoice Management: Generate and manage invoices seamlessly.</li> </ul> <p>Each section includes step-by-step instructions and explanations of the system's features and functionalities, ensuring you can navigate and utilize the system to its fullest potential.</p>"},{"location":"Installation/index.html","title":"Installation Overview","text":"<p>This guide provides detailed instructions for setting up a WinForms application that uses a database and the Google Maps Static API. The setup process involves:</p> <ol> <li>Configuring the application settings</li> <li>Obtaining a Google Maps Static API key</li> <li>Setting up a database (using either XAMPP or Docker)</li> <li>Running the WinForms application</li> </ol> <p>The database setup is a required step, and you must choose either Method 1 (XAMPP) or Method 2 (Docker) for this purpose.</p> <p>The Database Cluster Setup is optional and can be implemented for more advanced configurations.</p>"},{"location":"Installation/accessing-services.html","title":"Accessing Services","text":"<ul> <li>Main Application: Run <code>LMCIS-1DG4.exe</code></li> <li>PHPMyAdmin (XAMPP): phpMyAdmin</li> <li>PHPMyAdmin (Docker): phpMyAdmin Docker</li> </ul>"},{"location":"Installation/configuring-application.html","title":"Configuring Your Application","text":"<p>Update your <code>App.config</code> file with the appropriate connection string based on your chosen database setup method:</p> <p>For XAMPP:</p> <pre><code>&lt;add key=\"ConnectionString1\" value=\"server=localhost;port=3306;user id=root; password=;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30\"/&gt;\n</code></pre> <p>For Docker:</p> <pre><code>&lt;add key=\"ConnectionString1\" value=\"server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30\"/&gt;\n</code></pre>"},{"location":"Installation/database-cluster-setup.html","title":"Database Cluster Setup (Optional)","text":"<p>For a more robust setup, you can configure a database cluster:</p> <ol> <li>Modify the <code>docker-compose.yml</code> to include multiple database instances</li> <li>Set up a load balancer (e.g., HAProxy) to distribute requests</li> <li>Update the connection strings in <code>App.config</code> to point to the load balancer</li> </ol> <p>Example cluster setup in <code>docker-compose.yml</code>:</p> <pre><code>services:\n  db-master:\n    image: mariadb:10.5\n    environment:\n      MYSQL_ROOT_PASSWORD: rootpassword\n      MYSQL_DATABASE: itp4915m_se1d_group4\n    ports:\n      - \"3306:3306\"\n    volumes:\n      - ./sql-scripts:/docker-entrypoint-initdb.d\n\n  db-slave1:\n    image: mariadb:10.5\n    environment:\n      MYSQL_ROOT_PASSWORD: rootpassword\n    ports:\n      - \"3307:3306\"\n\n  db-slave2:\n    image: mariadb:10.5\n    environment:\n      MYSQL_ROOT_PASSWORD: rootpassword\n    ports:\n      - \"3308:3306\"\n\n  haproxy:\n    image: haproxy:latest\n    ports:\n      - \"3309:3306\"\n    volumes:\n      - ./haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg\n    depends_on:\n      - db-master\n      - db-slave1\n      - db-slave2\n</code></pre>"},{"location":"Installation/database-setup.html","title":"Database Setup (Required)","text":"<p>Choose one of the following methods to set up your database:</p>"},{"location":"Installation/database-setup.html#method-1-xampp","title":"Method 1: XAMPP","text":""},{"location":"Installation/database-setup.html#downloading-and-installing-xampp","title":"Downloading and Installing XAMPP","text":"<ol> <li>Visit the official XAMPP website: XAMPP</li> <li>Download and install XAMPP for your operating system</li> <li>Start XAMPP Control Panel</li> <li>Start Apache and MySQL services</li> </ol>"},{"location":"Installation/database-setup.html#configuring-database","title":"Configuring Database","text":"<ol> <li>Access phpMyAdmin: phpMyAdmin</li> <li>Create a new database named \"itp4915m_se1d_group4\"</li> <li>Import your SQL scripts to set up the database structure</li> </ol>"},{"location":"Installation/database-setup.html#method-2-docker","title":"Method 2: Docker","text":""},{"location":"Installation/database-setup.html#downloading-and-installing-docker","title":"Downloading and Installing Docker","text":"<ol> <li>Visit Docker Desktop</li> <li>Download and install Docker Desktop for your operating system</li> <li>Start Docker Desktop</li> </ol>"},{"location":"Installation/database-setup.html#configuring-docker-for-your-database","title":"Configuring Docker for Your Database","text":"<ol> <li>Create a <code>docker-compose.yml</code> file in your project root:</li> </ol> <pre><code>version: '3'\nservices:\n  db:\n    image: mariadb:10.5\n    environment:\n      MYSQL_ROOT_PASSWORD: rootpassword\n      MYSQL_DATABASE: itp4915m_se1d_group4\n    ports:\n      - \"3306:3306\"\n    volumes:\n      - ./sql-scripts:/docker-entrypoint-initdb.d\n  phpmyadmin:\n    image: phpmyadmin/phpmyadmin\n    ports:\n      - \"8080:80\"\n    environment:\n      PMA_HOST: db\n      PMA_USER: root\n      PMA_PASSWORD: rootpassword\n</code></pre> <ol> <li>Place your SQL scripts in a folder named <code>sql-scripts</code> in your project directory</li> <li>Open a terminal/command prompt in your project directory</li> <li>Run <code>docker-compose up -d</code></li> <li>Access phpMyAdmin at phpMyAdmin Docker to manage your database</li> </ol>"},{"location":"Installation/obtaining-api-key.html","title":"Obtaining Google Maps Static API Key","text":"<p>To use the Google Maps Static API in your application, you need to obtain an API key.</p> <p>Follow these steps:</p> <ol> <li>Go to the Google Cloud Console: https://console.cloud.google.com/</li> </ol> <p></p> <ol> <li>Create a new project or select an existing one.</li> </ol> <p></p> <ol> <li> <p>Enable the Google Maps Static API:</p> </li> <li> <p>In the sidebar, click on \"APIs &amp; Services\" &gt; \"Library\"</p> </li> <li>Search for \"Maps Static API\"</li> <li>Click on \"Maps Static API\" and then click \"Enable\"</li> </ol> <p></p> <ol> <li> <p>Create credentials for the API:</p> </li> <li> <p>In the sidebar, click on \"APIs &amp; Services\" &gt; \"Credentials\"</p> </li> <li>Click \"Create Credentials\" and select \"API Key\"</li> </ol> <p></p> <ol> <li> <p>Restrict the API key (recommended):</p> </li> <li> <p>In the API key details page, click \"Restrict Key\"</p> </li> <li>Under \"Application restrictions\", choose \"IP addresses\" and add your organization's ASN IP</li> <li>Under \"API restrictions\", select \"Restrict key\" and choose \"Maps Static API\"</li> </ol> <p></p> <ol> <li>Copy the API key and paste it into your <code>App.config</code> file:</li> </ol> <pre><code>&lt;add key=\"GoogleMapsApiKey\" value=\"YOUR_API_KEY_HERE\"/&gt;\n</code></pre>"},{"location":"Installation/prerequisites.html","title":"Prerequisites","text":"<ul> <li>.NET Framework (version compatible with your WinForms application)</li> <li>Google Maps Static API Key</li> <li>Database setup (choose either XAMPP or Docker)</li> </ul>"},{"location":"Installation/running-application.html","title":"Running Your WinForms Application","text":"<ol> <li>Ensure your chosen database environment (XAMPP or Docker) is running</li> <li>Navigate to the TemplateV1 folder</li> <li>Run <code>LMCIS-1DG4.exe</code></li> </ol>"},{"location":"Installation/setup.html","title":"Setup","text":""},{"location":"Installation/setup.html#clone-the-repository","title":"Clone the Repository","text":"<ol> <li>Clone the repository</li> <li>Copy the <code>App.config.example</code> to <code>App.config</code></li> </ol>"},{"location":"Installation/setup.html#customizing-appconfig","title":"Customizing App.config","text":"<p>The <code>App.config</code> file contains important settings for your application. Here's how to customize it:</p> <pre><code>&lt;appSettings&gt;\n    &lt;add key=\"DevMode\" value=\"True\"/&gt;\n    &lt;add key=\"GoogleMapsApiKey\" value=\"Enter your Google Maps Static API Key\"/&gt;\n    &lt;add key=\"ConnectionString1\" value=\"server=localhost;port=3306;user id=root; password=rootpassword;database=itp4915m_se1d_group4;charset=utf8;ConnectionTimeout=30\"/&gt;\n    &lt;!-- Additional connection strings if needed --&gt;\n&lt;/appSettings&gt;\n</code></pre>"},{"location":"Installation/setup.html#key-settings","title":"Key Settings","text":"<ol> <li>DevMode: Set to \"True\" for development, \"False\" for production</li> <li>GoogleMapsApiKey: Replace with your actual Google Maps Static API Key</li> <li>ConnectionString1: Customize based on your chosen database setup method</li> </ol>"},{"location":"Installation/stopping-application.html","title":"Stopping the Application","text":"<ol> <li>Close the WinForms application</li> <li>Open a terminal/command prompt in your project directory</li> <li>For Docker: Run <code>docker-compose down</code></li> <li>For XAMPP: Stop Apache and MySQL services in XAMPP Control Panel</li> </ol>"},{"location":"Installation/troubleshooting.html","title":"Troubleshooting","text":"<ul> <li>Verify database connection settings in <code>App.config</code></li> <li>Ensure the database service is running (XAMPP or Docker)</li> <li>Check that port 3306 is not being used by another application</li> <li>Validate your Google Maps API key</li> </ul> <p>Remember to keep sensitive information like API keys and passwords secure.</p>"},{"location":"UserGuide/index.html","title":"User Guide","text":""},{"location":"UserGuide/index.html#10-login-system-pages","title":"1.0 Login &amp; System Pages","text":""},{"location":"UserGuide/index.html#101-forget-password","title":"1.0.1 Forget Password","text":""},{"location":"UserGuide/index.html#102-create-customer-account","title":"1.0.2 Create Customer Account","text":"<p>Staff account can only be created by the department manager.</p> <p></p>"},{"location":"UserGuide/index.html#103-home-page","title":"1.0.3 Home Page","text":""},{"location":"UserGuide/index.html#1031-view-full-record","title":"1.0.3.1 View Full Record","text":"<p>All login records will be shown.</p> <p></p>"},{"location":"UserGuide/index.html#104-change-password","title":"1.0.4 Change Password","text":"<p>The change password function is on the profile page.</p> <p></p>"},{"location":"UserGuide/index.html#105-profile","title":"1.0.5 Profile","text":"<p>Staff\u2019s Profile:</p> <p></p> <p>Customer\u2019s Profile:</p> <p></p>"},{"location":"UserGuide/index.html#106-about-the-system","title":"1.0.6 About the System","text":"<p>The user can view the current system status by clicking the Legend Motor Company.</p> <p></p> <p></p>"},{"location":"UserGuide/feedback-management.html","title":"Feedback management","text":""},{"location":"UserGuide/feedback-management.html#110-give-feedback","title":"1.10 Give Feedback","text":"<p>Go to the give feedback section through the left-hand side button on the window.</p> <p> </p>"},{"location":"UserGuide/invoice-management.html","title":"Invoice Management","text":""},{"location":"UserGuide/invoice-management.html#111-view-invoice","title":"1.11 View Invoice","text":"<p>Go to a specific order\u2019s page.</p> <p></p> <p>The invoice of the order is displayed.</p> <p></p> <p>After the save as PDF button is clicked.</p> <p></p> <p>After the print invoice is clicked.</p> <p></p>"},{"location":"UserGuide/on-sale-product-management.html","title":"on sale product management","text":""},{"location":"UserGuide/on-sale-product-management.html#12-create-order","title":"1.2 Create Order","text":"<p>We can see that the spare part added is in the cart.</p> <p></p> <p></p> <p></p>"},{"location":"UserGuide/order-management.html","title":"Order Management","text":""},{"location":"UserGuide/order-management.html#13-search-order","title":"1.3 Search Order","text":""},{"location":"UserGuide/order-management.html#14-view-order","title":"1.4 View Order","text":"<p>After clicking \u201cView Order\u201d of a specific order, the user will be directed to the order\u2019s page.</p> <p></p>"},{"location":"UserGuide/order-management.html#15-cancel-order","title":"1.5 Cancel Order","text":""},{"location":"UserGuide/order-management.html#16-edit-order","title":"1.6 Edit Order","text":"<p>User will be directed to the edit order page.</p> <p></p> <p>After clicking the pen, a textbox and a trash can image are shown.</p> <p></p> <p>To add new spare parts to the order, go back to the spare part list.</p> <p></p> <p>Go into the spare part\u2019s detail info page.</p> <p></p> <p>After clicking the \u201cAdd to Existing Order\u201d button, the user will be directed to a new page.</p> <p></p> <p>Go back to the order\u2019s page.</p> <p></p> <p>To delete a spare part in the order, click the red trash can after clicking the pen.</p> <p></p>"},{"location":"UserGuide/order-management.html#17-re-order","title":"1.7 Re-Order","text":"<p>Go to a specific order\u2019s page first.</p> <p> </p> <p>Proceed to the cart to see the spare part in that order added to the cart.</p> <p></p>"},{"location":"UserGuide/order-management.html#112-staff-view-order","title":"1.12 Staff View Order","text":"<p>Users will be directed to the order\u2019s page after the \u201cView Order\u201d button is clicked.</p> <p></p>"},{"location":"UserGuide/order-management.html#113-staff-search-order","title":"1.13 Staff Search Order","text":""},{"location":"UserGuide/order-management.html#114-staff-confirm-order","title":"1.14 Staff Confirm Order","text":"<p>Go to a specific order page first.</p> <p></p>"},{"location":"UserGuide/order-management.html#115-staff-cancel-order","title":"1.15 Staff Cancel Order","text":"<p>Go to a specific order page first.</p> <p></p>"},{"location":"UserGuide/order-management.html#116-staff-edit-order","title":"1.16 Staff Edit Order","text":"<p>Go to a specific order page first.</p> <p>Users will be</p> <p>directed to the edit order page.</p> <p></p> <p>To edit an item, click the pencil of the item\u2019s row.</p> <p></p> <p>To add a spare part to the order, click \u201cAdd New Part\u201d in the edit order page.</p> <p></p>"},{"location":"UserGuide/stock-management.html","title":"Stock Management","text":""},{"location":"UserGuide/stock-management.html#11-browse-spare-parts","title":"1.1 Browse Spare Parts","text":"<p>After clicking the \u201cView\u201d button, it will go to the page that contains detailed information of the selected spare part.</p> <p></p>"},{"location":"UserGuide/stock-management.html#18-add-to-favourite","title":"1.8 Add to Favourite","text":"<p>Go to the specific spare part page first.</p> <p></p> <p></p>"},{"location":"UserGuide/stock-management.html#19-remove-from-favourite","title":"1.9 Remove from Favourite","text":"<p>Go to the spare part that is favourite.</p> <p></p> <p>Alternatively, users can go to the favorite section to remove the spare part from favorites.</p> <p></p>"},{"location":"UserGuide/user-management.html","title":"1.22 Staff Add New Staff","text":"<p>Go to the add new staff page.</p> <p></p> <p>After clicking \u201cCreate Staff Account,\u201d the new staff will be added to the system.</p> <p></p>"},{"location":"UserGuide/user-management.html#123-staff-search-staff","title":"1.23 Staff Search Staff","text":"<p>The search staff page.</p> <p></p>"},{"location":"UserGuide/user-management.html#124-staff-view-staff","title":"1.24 Staff View Staff","text":"<p>The specific staff info page.</p> <p></p>"},{"location":"UserGuide/user-management.html#125-staff-update-staff","title":"1.25 Staff Update Staff","text":"<p>The update staff page.</p> <p></p>"}]}