# Use an official Python runtime as a parent image
FROM python:3.9-slim

# Install necessary system packages
RUN apt-get update && apt-get install -y supervisor

# Set the working directory in the container
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY /MoveFiles/. /app

# Install Python dependencies
RUN pip install --no-cache-dir -r requirements.txt

# Copy the supervisor configuration file
COPY /MoveFiles/supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# Run supervisor when the container launches
CMD ["/usr/bin/supervisord"]
