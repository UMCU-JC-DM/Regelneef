# Use an official Python runtime as a parent image
FROM python:3.9-slim

# Install inotify-tools and supervisor
RUN apt-get update && apt-get install -y inotify-tools supervisor

# Set the working directory in the container
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY . /app

# Install any needed packages specified in requirements.txt
# RUN pip install --no-cache-dir -r requirements.txt

# Copy the supervisor configuration file
COPY ./MoveFiles/supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# Run supervisor when the container launches
CMD ["/usr/bin/supervisord"]
