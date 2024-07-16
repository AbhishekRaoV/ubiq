provider "aws" {
  region = "us-east-1"  // Replace with your desired AWS region
}

resource "aws_instance" "example" {
  ami           = "ami-09e67e426f25ce0d7" // Ubuntu 20.04 LTS 64-bit AMI, you may need to change this for other regions
  instance_type = "t2.micro"
  key_name      = "nextgen-devops-team"     // Replace with your SSH key pair name
  tags = {
    Name = "auctane-demo"
  }
}

output "instance_ip" {
  value = aws_instance.example.public_ip
}
