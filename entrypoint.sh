#!/bin/bash
sed  "s/path.*/path\":  \"\/Logs\/iikotransport\/`hostname`-.log\", /g" appsettings.k8s-template.json > appsettings.json || true
exec "$@"
